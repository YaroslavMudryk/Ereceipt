using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Users;
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
        private readonly IAuthenticationService _authenticationService;
        public IdentityController(ITokenManager tokenManager, IAuthenticationService authenticationService)
        {
            _tokenManager = tokenManager;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterEmailCreateModel model)
        {
            var res = await _authenticationService.RegisterByEmailAsync(model);
            if (res.IsSuccessed)
                return Ok();
            return BadRequest(res.Error);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmAsync([FromBody] ConfirmEmailCreateModel model)
        {
            var res = await _authenticationService.ConfirmUserAsync(model);
            if (res.IsSuccessed)
                return Ok();
            return BadRequest(res.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginEmailCreateModel model)
        {
            var res = await _authenticationService.LoginByEmailAsync(model);
            if (res.IsSuccessed)
                return Ok(res.Data);
            return BadRequest(res.Error);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var res = await _authenticationService.LogoutByTokenAsync(GetRequestData());
            return Ok();
        }
    }
}
