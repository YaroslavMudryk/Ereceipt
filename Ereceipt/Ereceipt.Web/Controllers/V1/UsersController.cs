using Ereceipt.Application.Services.Interfaces;
using Extensions.Generator;
using Microsoft.AspNetCore.Mvc;

namespace Ereceipt.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("creds")]
        public IActionResult GetAppId()
        {
            return Ok(new
            {
                AppId = RandomGenerator.GetUniqCode(5).ToUpper(),
                AppSecret = RandomGenerator.GetString(50).ToLower()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page = 1)
        {
            if (page < 1)
                return BadRequest();
            return Ok(await _userService.GetAllUsersAsync(page));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string q, int page = 1)
        {
            if(q == null)
                return await GetAllUsers(page);
            return Ok(await _userService.SearchUsersAsync(q, page));
        }
    }
}
