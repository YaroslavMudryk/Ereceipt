using AutoMapper;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Authentication;
using Ereceipt.Constants;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions.Password;
using Extensions.Generator;
using System.Threading.Tasks;

namespace Ereceipt.Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly EreceiptContext _db;
        public AuthenticationService(IMapper mapper, EreceiptContext context)
        {
            _mapper = mapper;
            _db = context;
        }

        public async Task<Result<ConfirmEmailViewModel>> ConfirmUserAsync(ConfirmEmailCreateModel model)
        {
            var userLoginForConfirm = await _db.UserLogins.FirstOrDefaultAsync(x => x.Login == model.Login && x.Type == "email");
            if (userLoginForConfirm is null)
                return new Result<ConfirmEmailViewModel>(Errors.Users.UserNotExist);
            if (userLoginForConfirm.IsConfirm)
                return new Result<ConfirmEmailViewModel>(Errors.Users.UserAlreadyConfirmed);
            var diffTime = DateTime.Now - userLoginForConfirm.CreatedAt;
            if (diffTime.Days > 1)
                return new Result<ConfirmEmailViewModel>(Errors.Users.TokenExpired);
            if (userLoginForConfirm.TokenConfirm != model.Token)
                return new Result<ConfirmEmailViewModel>(Errors.Users.IncorrectToken);
            userLoginForConfirm.IsConfirm = true;
            userLoginForConfirm.ConfirmAt = DateTime.Now;
            var confirmResult = await UpdateUserLoginAsync(userLoginForConfirm);
            if (!confirmResult.IsConfirm)
                return new Result<ConfirmEmailViewModel>(Errors.Users.SomethingWrong);

            //todo send notification

            return new Result<ConfirmEmailViewModel>(result: null);
        }

        private async Task<UserLogin> UpdateUserLoginAsync(UserLogin userLogin)
        {
            _db.UserLogins.Update(userLogin);
            await _db.SaveChangesAsync();
            return userLogin;
        }

        public async Task<Result<UserLoginViewModel>> LoginByEmailAsync(LoginEmailCreateModel model)
        {
            var app = await _db.Apps.FirstOrDefaultAsync(x => x.AppId == model.AppId);
            if (app is null)
            {
                return new Result<UserLoginViewModel>("App not found");
            }
            if (app.AppSecret != model.AppSecret)
            {
                return new Result<UserLoginViewModel>("App not found");
            }
            if (app.InDevelopment)
            {
                return new Result<UserLoginViewModel>("App not available");
            }
            if (!app.IsActiveByDate)
            {
                return new Result<UserLoginViewModel>("App is expired");
            }




            var userLogin = await _db.UserLogins.FirstOrDefaultAsync(x => x.Login == model.Login && x.Type == "email");
            if (userLogin is null)
            {
                return new Result<UserLoginViewModel>("User not found");
            }
            if (!PasswordManager.VerifyPasswordHash(model.Password, userLogin.Password))
            {
                return new Result<UserLoginViewModel>("Password is incorrect");
            }
            if (!userLogin.IsConfirm)
            {
                return new Result<UserLoginViewModel>("User not confirm");
            }




            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == userLogin.UserId);

            var roles = await _db.UserRoles
                .AsNoTracking()
                .Include(x=>x.Role)
                .Where(x=>x.UserId== userLogin.UserId)
                .Select(x=>x.Role)
                .ToListAsync();

            var newSession = GetSession(app, model, currentUser.Id);

            var dataForLogin = new UserLoginViewModel
            {
                LoginData = userLogin,
                Session = _mapper.Map<SessionViewModel>(newSession),
                User = currentUser,
                Role = roles.OrderByDescending(x => x.AccessLevel).ToArray()[0]
            };
            dataForLogin.Token = _claimsProvider.GenerateAccessToken(_claimsProvider.GetClaimsIdentity(dataForLogin));

            dataForLogin.Token.User = new UserAuthViewModel
            {
                AppName = app.Name,
                Id = currentUser.Id,
                AuthMethod = userLogin.Type,
                Avatar = currentUser.Avatar,
                Device = newSession.Device,
                Firstname = currentUser.Name.Split(' ')[0],
                Lastname = currentUser.Name.Split(' ')[1],
                Fullname = currentUser.Name,
                Platform = newSession.Platform,
                Role = dataForLogin.Role.Name,
                Username = currentUser.Username
            };

            newSession.Claims = CustomMapper.ConvertClaimsToJson(dataForLogin.Token.Token.Claims);
            newSession.Token = dataForLogin.Token.Token.Token;
            var sessionToView = await _sessionRepository.CreateAsync(newSession);
            if (sessionToView == null || !sessionToView.IsActive)
            {
                await _loginAttemptRepository.CreateAsync(GetLoginAttempt(CauseStatus.FailedSessionActivate, LoginStatus.Failed, model));
                return new Result<UserLoginViewModel>("Session activate attempt failed");
            }

            await _sessionsManager.AddNewSessionAsync(newSession.Token);



            await _notificationService.CreateNotificationAsync(_notificationHelper.GetLoginNotification(currentUser.Id, newSession, model, app));



            await _loginAttemptRepository.CreateAsync(GetLoginAttempt(null, LoginStatus.Success, model));
            return new Result<UserLoginViewModel>(dataForLogin);
        }

        public async Task<Result<UserRegisterViewModel>> RegisterByEmailAsync(RegisterEmailCreateModel model)
        {
            var checkUser = await _db.UserLogins.FirstOrDefaultAsync(x => x.Login == model.Login && x.Type == "email");
            if (checkUser != null)
                return new Result<UserRegisterViewModel>(Errors.Users.UserAlreadyExist);
            var checkApp = await _db.Apps.FirstOrDefaultAsync(x => x.AppId == model.AdditionalInfo.AppId);
            if (checkApp is null)
                return new Result<UserRegisterViewModel>("App not found");
            if (checkApp.AppSecret != model.AdditionalInfo.AppSecret)
                return new Result<UserRegisterViewModel>("AppSecret is incorrect");

            var newUserLogin = new UserLogin
            {
                Login = model.Login,
                Password = model.Password.GeneratePasswordHash(),
                CreatedAt = DateTime.Now,
                CreatedBy = "0",
                CreatedFromIP = model.IP,
                ConfirmAt = null,
                IsConfirm = false,
                TokenConfirm = RandomGenerator.GetString(50),
                Type = "email",
                Version = 0,
                User = new User
                {
                    Name = $"{model.FirstName} {model.LastName}",
                    CreatedAt = DateTime.Now,
                    Version = 0,
                    CreatedBy = "0",
                    Username = $"{Guid.NewGuid().ToString("N").Substring(0, 10)}"
                }
            };

            var createdUserLogin = await CreateUserLoginAsync(newUserLogin);
            if (createdUserLogin == null)
                return new Result<UserRegisterViewModel>("Something went wrong");

            await CreateUserRoleAsync(new UserRole
            {
                UserId = createdUserLogin.UserId,
                RoleId = 1
            });

            //todo notification

            return new Result<UserRegisterViewModel>(result: null);
        }


        private async Task<UserLogin> CreateUserLoginAsync(UserLogin userLogin)
        {
            await _db.UserLogins.AddAsync(userLogin);
            await _db.SaveChangesAsync();
            return userLogin;
        }

        private async Task<UserRole> CreateUserRoleAsync(UserRole userRole)
        {
            await _db.UserRoles.AddAsync(userRole);
            await _db.SaveChangesAsync();
            return userRole;
        }

        private Session GetSession(App app, LoginEmailCreateModel loginEmailCreateModel, int userId)
        {
            return new Session
            {
                AppId = app.AppId,
                AppName = app.Name,
                AppVersion = loginEmailCreateModel.AppVersion,
                Country = loginEmailCreateModel.UserInfo.Location.Country,
                City = loginEmailCreateModel.UserInfo.Location.City,
                CreatedAt = DateTime.Now,
                CreatedBy = "0",
                CreatedFromIP = loginEmailCreateModel.IP,
                DateUnActive = null,
                DeviceType = loginEmailCreateModel.DeviceType,
                Device = loginEmailCreateModel.Device,
                IsActive = true,
                IsOfficialApp = app.IsOfficial,
                Lat = loginEmailCreateModel.UserInfo.Location.Lat,
                Lon = loginEmailCreateModel.UserInfo.Location.Lon,
                IP = loginEmailCreateModel.UserInfo.Location.IP,
                Platform = loginEmailCreateModel.Platform,
                Region = loginEmailCreateModel.UserInfo.Location.Region,
                UserId = userId,
                Version = 0
            };
        }
    }
}
