using Ereceipt.Application.ViewModels.Users;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<UserLoginViewModel>> LoginByEmailAsync(LoginEmailCreateModel model);
        Task<Result<RegisterEmailCreateModel>> RegisterByEmailAsync(RegisterEmailCreateModel model);
        Task<Result<ConfirmEmailCreateModel>> ConfirmUserAsync(ConfirmEmailCreateModel model);
    }
}
