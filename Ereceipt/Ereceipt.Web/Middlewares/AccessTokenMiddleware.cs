namespace Ereceipt.Web.Middlewares
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string headerName = "Authorization";
        public AccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var token = httpContext.Request.Query["access_token"].ToString();
            if (string.IsNullOrEmpty(httpContext.Request.Headers[headerName]) && !string.IsNullOrEmpty(token))
            {
                httpContext.Request.Headers[headerName] = $"Bearer {token}";
            }
            await _next(httpContext);
        }
    }

    public static class AccessTokenMiddlewareExtensions
    {
        public static void UseAccessToken(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AccessTokenMiddleware>();
        }
    }
}