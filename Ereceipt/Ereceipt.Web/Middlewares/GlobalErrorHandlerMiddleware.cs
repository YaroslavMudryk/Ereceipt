using Ereceipt.Web.AppSettings;
using System.Net;
namespace Ereceipt.Web.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
                return;
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var requestId = httpContext.TraceIdentifier;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsJsonAsync(new ApiResponse
            {
                Ok = false,
                Error = "Internal server error",
                Data = requestId
            });
        }
    }

    public static class GlobalErrorHandlerMiddlewareExtensions
    {
        public static void UseGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}