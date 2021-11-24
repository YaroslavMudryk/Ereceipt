using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Users;
using Ereceipt.Constants;
using Ereceipt.Domain.Models;
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

        public AuthViewModel GenerateAccessToken(TokenDataViewModel tokenData)
        {
            var claims = GetUserClaims(tokenData);
            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromDays(TokenOptions.LIFETIME));
            var jwt = new JwtSecurityToken(
                    issuer: TokenOptions.ISSUER,
                    audience: TokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
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
                    Claims = claims.Select(x => new ClaimViewModel
                    {
                        Type = x.Type,
                        Value = x.Value
                    }).ToList()
                }
            };
        }

        private List<Claim> GetUserClaims(TokenDataViewModel tokenData)
        {
            var claims = new List<Claim>(9);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, tokenData.User.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, tokenData.User.Username));
            claims.Add(new Claim(ClaimTypes.Email, tokenData.UserLogin.Login));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "pwd"));
            claims.Add(GetRoleClaim(tokenData));
            claims.Add(new Claim("AppName", tokenData.App.Name));
            //claims.Add(new Claim("Device", GetDeviceNameClaim(tokenData.Session)));
            claims.Add(new Claim("DeviceType", GetDeviceTypeClaim(tokenData.Session)));
            claims.Add(new Claim("Platform", GetOsClaim(tokenData.Session)));
            return claims;
        }

        private string GetDeviceNameClaim(Session session)
        {
            string device = null;
            var brand = session.Device.Device.Brand;
            var model = session.Device.Device.Model;
            if (brand == null || model == null)
            {
                if(brand == null)
                {
                    device = model;
                }
                else
                {
                    device = brand;
                }
            }
            else
            {
                device = $"{brand} {model}";
            }
            return device;
        }
        private string GetDeviceTypeClaim(Session session)
        {
            var type = session.Device.Device.Type;
            if (string.IsNullOrEmpty(type))
                return "Unknown";
            return type;
        }
        private string GetOsClaim(Session session)
        {
            string os = null;
            var osName = session.Device.OS.Name;
            var osVersion = session.Device.OS.Version;

            if (osName == null || osVersion == null)
                os = "Unknown";
            else
                os = $"{osName} {osVersion}";

            return os;
        }
        private Claim GetRoleClaim(TokenDataViewModel tokenData)
        {
            Claim claim = null;
            if (tokenData.Roles != null && tokenData.Roles.Count > 1)
                claim = new Claim(ClaimTypes.Role, tokenData.Roles.OrderByDescending(x => x.Lvl).FirstOrDefault().Name);
            else
                claim = new Claim(ClaimTypes.Role, tokenData.Roles[0].Name);
            return claim;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }

        public T GetValueByType<T>(string type)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == type);
            if (claim == null)
                return default(T);
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }
    }
}
