using Microsoft.AspNetCore.Builder;

namespace UserList.Helpers.Middlewares
{
    public static class ErrorLogMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLogMiddleware>();
        }
    }
}
