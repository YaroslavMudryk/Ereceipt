using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Users;
using Ereceipt.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ereceipt.Application.Services.Implementations
{
    public class ClaimsProvider : IClaimsProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public ClaimsProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public AuthViewModel GenerateAccessToken(AuthenticationViewModel authentication)
        {
            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromDays(TokenOptions.LIFETIME));
            var jwt = new JwtSecurityToken(
                    issuer: TokenOptions.ISSUER,
                    audience: TokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: authentication.Claims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AuthViewModel
            {
                Token = new TokenViewModel
                {
                    Token = token,
                    Type = "bearer",
                    ExpiredAt = expires,
                    Claims = authentication.Claims.Select(x => new ClaimViewModel
                    {
                        Type = x.Type,
                        Value = x.Value
                    }).ToList()
                }
            };
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }

        public AuthenticationViewModel GetClaimsIdentity(UserLoginViewModel loginModel)
        {
            if (loginModel is null)
                return new AuthenticationViewModel
                {
                    Claims = null,
                    Error = "Some error"
                };
            var claims = new List<Claim>(5);
            claims.Add(new Claim("Id", loginModel.User.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, loginModel.User.Username));
            claims.Add(new Claim(ClaimTypes.Role, loginModel.Role.Name));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, loginModel.LoginData.Type));
            claims.Add(new Claim("Device", loginModel.Session.Device.Device));
            claims.Add(new Claim("Platform", loginModel.Session.Device.Platform));
            claims.Add(new Claim("AppName", loginModel.Session.Application.AppName));
            return new AuthenticationViewModel
            {
                Error = null,
                Claims = claims
            };
        }

        public string GetValueByType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
