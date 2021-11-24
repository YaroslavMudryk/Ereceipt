using Ereceipt.Application.ViewModels.Users;
using Microsoft.AspNetCore.Http;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IClaimsProvider
    {
        AuthViewModel GenerateAccessToken(TokenDataViewModel tokenData);
        Task<string> GetAccessTokenAsync();
        T GetValueByType<T>(string type);
        HttpContext HttpContext { get; }
    }
}
