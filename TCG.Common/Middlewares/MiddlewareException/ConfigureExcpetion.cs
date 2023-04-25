using Microsoft.AspNetCore.Builder;

namespace TCG.Common.Middlewares.MiddlewareException
{
    public static class ConfigureException
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
