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
        private readonly ISessionService _sessionService;
        public IdentityController(ITokenManager tokenManager, IAuthenticationService authenticationService, ISessionService sessionService)
        {
            _tokenManager = tokenManager;
            _authenticationService = authenticationService;
            _sessionService = sessionService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterEmailCreateModel model)
        {
            var res = await _authenticationService.RegisterByEmailAsync(model);
            if (res.IsSuccessed)
                return ReturnOk(null);
            return ReturnBadRequest(res.Error);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmAsync([FromBody] ConfirmEmailCreateModel model)
        {
            var res = await _authenticationService.ConfirmUserAsync(model);
            if (res.IsSuccessed)
                return ReturnOk(null);
            return ReturnBadRequest(res.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginEmailCreateModel model)
        {
            var res = await _authenticationService.LoginByEmailAsync(model);
            if (res.IsSuccessed)
                return ReturnOk(res.Data);
            return ReturnBadRequest(res.Error);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            var res = await _authenticationService.LogoutByTokenAsync(GetRequestData());
            return ReturnOk(null);
        }

        [HttpGet("sessions")]
        [Authorize]
        public async Task<IActionResult> GetMySessionsAsync()
        {
            var res = await _sessionService.GetUserSessionsAsync(GetRequestData().UserId);
            if (res.IsSuccessed)
                return ReturnOk(res.Data);
            return ReturnBadRequest(res.Error);
        }
    }
}
