using Microsoft.AspNetCore.Builder;

namespace Ntils.Middlewares
{
    public static class WebTraceMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTracer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebTraceMiddleware>();
        }
    }
}