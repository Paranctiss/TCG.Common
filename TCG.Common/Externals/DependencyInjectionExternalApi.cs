using Microsoft.Extensions.DependencyInjection;

namespace TCG.Common.Externals;

public static class DependencyInjectionExternalApi
{
    public static IServiceCollection AddExternals<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddHttpClient(); // Enregistrer l'instance d'HttpClient comme singleton
        services.AddTransient<TService, TImplementation>();
        return services;
    }
}