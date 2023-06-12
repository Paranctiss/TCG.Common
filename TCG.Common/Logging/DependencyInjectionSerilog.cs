using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TCG.Common.Logging;

public static class DependencyInjectionSerilog
{
    /// <summary>
    /// Adds configured Serilog logging with client infos.
    /// </summary>
    /// <param name="webApplicationBuilder">App builder.</param>
    /// <returns>App builder.</returns>
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Logging.ClearProviders();
        webApplicationBuilder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                // .Enrich.WithHttpContext(services)
                .Enrich.WithClientAgent()
                .ReadFrom.Configuration(context.Configuration);
        });
        
        return webApplicationBuilder;
    }
}