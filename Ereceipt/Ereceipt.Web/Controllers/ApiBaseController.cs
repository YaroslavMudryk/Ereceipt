using Ereceipt.Application.ViewModels;
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
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Value == ClaimTypes.NameIdentifier).Value;
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
    }
}
