using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ereceipt.Web.Filters
{
    public class AuthenticationFilter : IAuthorizationFilter
    {
        private readonly ITokenManager _tokenManager;
        public AuthenticationFilter(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }


        public async void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var context = filterContext.HttpContext;
            if (context.IsAuthenticationRequired())
            {
                var token = await context.GetTokenAsync("access_token");
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    if (_tokenManager.IsAvailableToken(token))
                    {
                        if (!context.User.Identity.IsAuthenticated)
                        {
                            _tokenManager.RemoveToken(token);
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                    }
                }
            }
        }
    }
}
