using AutoMapper;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Authentication;
using Ereceipt.Application.ViewModels.Users;
using Ereceipt.Constants;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Extensions.Generator;
using Extensions.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ereceipt.Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IDetectClient _detectClient;
        private readonly EreceiptContext _db;
        public AuthenticationService(IMapper mapper, EreceiptContext context, IDetectClient detectClient)
        {
            _mapper = mapper;
            _db = context;
            _detectClient = detectClient;
        }

        public async Task<Result<ConfirmEmailCreateModel>> ConfirmUserAsync(ConfirmEmailCreateModel model)
        {
            var userForConfirm = await _db.UserLogins
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == model.EmailAddress && x.Type == UserLoginType.Email);
            if (userForConfirm == null)
                return new Result<ConfirmEmailCreateModel>("User not exist");
            if (userForConfirm.IsConfirm)
                return new Result<ConfirmEmailCreateModel>("User is already confirmed");
            if (userForConfirm.TokenConfirm != model.Token)
                return new Result<ConfirmEmailCreateModel>("Token not confirmed");

            userForConfirm.IsConfirm = true;
            userForConfirm.ConfirmAt = DateTime.Now;
            userForConfirm.ConfirmFromDevice = _detectClient.GetClientInfo(model);

            _db.UserLogins.Update(userForConfirm);
            await _db.SaveChangesAsync();

            return new Result<ConfirmEmailCreateModel>();
        }

        public async Task<Result<UserLoginViewModel>> LoginByEmailAsync(LoginEmailCreateModel model)
        {
            var userLogin = await _db.UserLogins.AsNoTracking().FirstOrDefaultAsync(x => x.Login == model.Login && x.Type == UserLoginType.Email);
            if (userLogin == null)
                return new Result<UserLoginViewModel>("User with this login not exist");

            var app = await _db.Apps.AsNoTracking().FirstOrDefaultAsync(x => x.AppId == model.App.AppId);
            if (app == null)
                return new Result<UserLoginViewModel>("App not found");
            if (app.AppSecret != model.App.AppSecret)
                return new Result<UserLoginViewModel>("App creds not real");
            if (!app.IsActiveByDate)
                return new Result<UserLoginViewModel>("App is expiration by date");
            if (app.InDevelopment)
                if (!app.CanUseWhileDevelopment.Contains(userLogin.UserId))
                    return new Result<UserLoginViewModel>("App in development");

            if (!userLogin.IsConfirm)
                return new Result<UserLoginViewModel>("User not confirm");

            if (!model.Password.VerifyPasswordHash(userLogin.PasswordHash))
                return new Result<UserLoginViewModel>("Login or password is incorrect");

            var clientInfo = _detectClient.GetClientInfo(model);

            var currentUser = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userLogin.UserId);

            var userRoles = await _db.UserRoles.Include(x => x.Role).Where(x => x.UserId == userLogin.UserId).Select(x => x.Role).ToListAsync();

            //todo rewrite current logic
            //todo rewrite current logic
            //todo rewrite current logic

            var dataForLogin = new UserLoginViewModel
            {
                LoginData = userLogin,
                //Session = _mapper.Map<SessionViewModel>(newSession),
                User = currentUser,
                Role = userRoles.OrderByDescending(x => x.Lvl).ToArray()[0]
            };

            //todo rewrite current logic
            //todo rewrite current logic
            //todo rewrite current logic


            return new Result<UserLoginViewModel>(dataForLogin);
        }

        public async Task<Result<RegisterEmailCreateModel>> RegisterByEmailAsync(RegisterEmailCreateModel model)
        {
            if (await IsExistUserAsync(model.Login))
                return new Result<RegisterEmailCreateModel>("User is already exist");

            if (await IsUsernameBusyAsync(model.Username))
                return new Result<RegisterEmailCreateModel>("Username is busy");

            var newUser = new User
            {
                CreatedAt = DateTime.Now,
                Name = model.Name,
                Username = model.Username,
                About = model.About
            };
            var newUserLogin = new UserLogin
            {
                CreatedAt = DateTime.Now,
                CreatedBy = "s",
                IsConfirm = false,
                Type = UserLoginType.Email,
                TokenConfirm = RandomGenerator.GetString(50, IsLowwer: true),
                Version = 0,
                Login = model.Login,
                PasswordHash = model.Password.GeneratePasswordHash(),
                User = newUser,
                RegisterFromDevice = _detectClient.GetClientInfo(model)
            };

            //todo create notification

            //todo send confirmation account to email

            await _db.UserLogins.AddAsync(newUserLogin);
            await _db.SaveChangesAsync();
            return new Result<RegisterEmailCreateModel>();
        }

        private async Task<bool> IsExistUserAsync(string email)
        {
            return await _db.UserLogins.AsNoTracking().AnyAsync(x => x.Login == email && x.Type == "email");
        }

        private async Task<bool> IsUsernameBusyAsync(string username)
        {
            return await _db.Users.AsNoTracking().AnyAsync(x => x.Username == username);
        }
    }
}
