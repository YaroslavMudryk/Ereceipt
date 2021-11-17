using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using System.Net;
namespace Ereceipt.Web.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenManager _tokenManager;
        public TokenMiddleware(RequestDelegate next, ITokenManager tokenManager)
        {
            _next = next;
            _tokenManager = tokenManager;
        }


        public async Task Invoke(HttpContext context)
        {
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
                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class TokenMiddlewareExtensions
    {
        public static void UseTokenManager(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<TokenMiddleware>();
        }
    }
}