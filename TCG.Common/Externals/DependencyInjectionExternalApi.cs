using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace TCG.Common.Externals;

public static class DependencyInjectionExternalApi
{
    public static IServiceCollection AddExternals<TService, TImplementation>(this IServiceCollection services, string baseUrl)
        where TService : class
        where TImplementation : class, TService
    {
        var clientName = typeof(TService).FullName;
        services.AddHttpClient<TService, TImplementation>(clientName, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddHttpClient(); // Enregistrer l'instance d'HttpClient comme singleton
        services.AddTransient<TService, TImplementation>();
        return services;
    }
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(4, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"[Retry Policy] Tentative {retryCount} échouée. Attente de {timeSpan}. Erreur: {exception.Exception.Message}");
                });
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .Or<HttpRequestException>()
            .Or<SocketException>()
            .CircuitBreakerAsync(4, TimeSpan.FromSeconds(40),
                onBreak: (exception, timeSpan) =>
                {
                   Console.WriteLine($"[Circuit Breaker Policy] Circuit ouvert à cause de: {exception}. Il restera ouvert pour: {timeSpan}.");
                },
                onReset: () =>
                {
                    Console.WriteLine($"[Circuit Breaker Policy] Circuit réinitialisé et fermé.");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine($"[Circuit Breaker Policy] Circuit en état half-open. La prochaine requête déterminera si le circuit doit être fermé ou reste ouvert.");
                });    }

}