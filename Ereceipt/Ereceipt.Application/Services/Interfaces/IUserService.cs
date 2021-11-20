using Ereceipt.Application.ViewModels.Users;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        Task<Result<UserViewModel>> GetUserByIdAsync(int id);
        Task<Result<List<UserShortViewModel>>> SearchUsersAsync(string query, int page);
        Task<Result<UsersSearch>> GetAllUsersAsync(int page);
    }
}