using Ereceipt.Application.ViewModels;
using Ereceipt.Web.AppSettings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ereceipt.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected RequestModel GetRequestData()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                return new RequestModel
                {
                    IP = ip,
                    UserId = Convert.ToInt32(userId)
                };
            }
            return new RequestModel
            {
                IP = HttpContext.Connection.RemoteIpAddress.ToString(),
                UserId = 0
            };
        }


        protected IActionResult ReturnOk(object data)
        {
            return Ok(new ApiResponse
            {
                Ok = true,
                Error = null,
                Data = data
            });
        }

        protected IActionResult ReturnBadRequest(string error)
        {
            return BadRequest(new ApiResponse
            {
                Ok = true,
                Error = error,
                Data = null
            });
        }

        protected IActionResult ReturnNotFound(string error = "Resource not found")
        {
            return NotFound(new ApiResponse
            {
                Ok = false,
                Error = error,
                Data = null
            });
        }
    }
}
