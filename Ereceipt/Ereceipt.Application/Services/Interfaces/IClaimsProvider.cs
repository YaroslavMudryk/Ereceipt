using Ereceipt.Application.ViewModels.Users;
using Microsoft.AspNetCore.Http;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IClaimsProvider
    {
        AuthenticationViewModel GetClaimsIdentity(UserLoginViewModel loginModel);
        AuthViewModel GenerateAccessToken(AuthenticationViewModel authentication);
        Task<string> GetAccessTokenAsync();
        string GetValueByType(string type);
        HttpContext HttpContext { get; }
    }
}
