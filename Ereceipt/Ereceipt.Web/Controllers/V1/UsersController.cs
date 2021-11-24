using Ereceipt.Application.Services.Interfaces;
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


        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page = 1)
        {
            if (page < 1)
                return ReturnBadRequest("Page must be more then 0");
            return ReturnOk(await _userService.GetAllUsersAsync(page));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (!user.IsSuccessed)
                return ReturnNotFound(user.Error);
            return ReturnOk(user.Data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string q, int page = 1)
        {
            if (string.IsNullOrEmpty(q))
                return ReturnBadRequest("Query ");
            var users = await _userService.SearchUsersAsync(q, page);
            return ReturnOk(users.Data);
        }
    }
}
