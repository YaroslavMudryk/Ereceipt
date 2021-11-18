using AutoMapper;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Users;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ereceipt.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly EreceiptContext _db;
        private readonly IMapper _mapper;
        public UserService(EreceiptContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<UsersSearch>> GetAllUsersAsync(int page)
        {
            int countPerRequest = 1000;
            int offset = (page - 1) * countPerRequest;
            var users = await _db.Users
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offset).Take(countPerRequest)
                .Select(x => new UserShortViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Avatar = x.Avatar,
                })
                .ToListAsync();
            var totalCount = await _db.Users.CountAsync();
            return new Result<UsersSearch>(new UsersSearch
            {
                Users = users,
                TotalCount = totalCount
            });
        }

        public async Task<Result<UserViewModel>> GetUserByIdAsync(int id)
        {
            var userFromDb = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (userFromDb == null)
                return new Result<UserViewModel>("User not found");
            var userToView = _mapper.Map<UserViewModel>(userFromDb);
            return new Result<UserViewModel>(userToView);
        }

        public async Task<Result<List<UserShortViewModel>>> SearchUsersAsync(string query, int page)
        {
            int countPerRequest = 20;
            int offset = (page - 1) * countPerRequest;
            var users = await _db.Users
                .AsNoTracking()
                .Where(x => x.Name.Contains(query))
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offset).Take(countPerRequest)
                .Select(x => new UserShortViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Avatar = x.Avatar,
                })
                .ToListAsync();
            if (!users.Any())
                return new Result<List<UserShortViewModel>>("Users not found");
            return new Result<List<UserShortViewModel>>(users);
        }
    }
}
