using Ereceipt.Application.ViewModels.Authentication;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<UserLoginViewModel>> LoginByEmailAsync(LoginEmailCreateModel model);
        Task<Result<UserRegisterViewModel>> RegisterByEmailAsync(RegisterEmailCreateModel model);
        Task<Result<ConfirmEmailViewModel>> ConfirmUserAsync(ConfirmEmailCreateModel model);
    }
}
