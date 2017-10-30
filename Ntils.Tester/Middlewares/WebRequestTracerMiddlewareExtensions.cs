using Microsoft.AspNetCore.Builder;

namespace Ntils.Middlewares
{
    public static class WebRequestTracerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTracer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebRequestTraceMiddleware>();
        }
    }
}