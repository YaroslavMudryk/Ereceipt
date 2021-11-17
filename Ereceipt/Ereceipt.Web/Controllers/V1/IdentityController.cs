using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Web.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ereceipt.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    public class IdentityController : ApiBaseController
    {
        private readonly ITokenManager _tokenManager;
        public IdentityController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "admin@admin"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthenticationOption.ISSUER,
                    audience: AuthenticationOption.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthenticationOption.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthenticationOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            _tokenManager.AddNewToken(encodedJwt);
            return Ok(new
            {
                token = encodedJwt,
                validTo = jwt.ValidTo
            });
        }
    }
}
