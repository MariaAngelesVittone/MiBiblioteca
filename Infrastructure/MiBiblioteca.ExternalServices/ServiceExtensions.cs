using Microsoft.Extensions.DependencyInjection;
using MiBiblioteca.Application.Interfaces.Services;
using Polly;
using Polly.Extensions.Http;

namespace MiBiblioteca.ExternalServices
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IBookMetadataProvider, OpenLibraryClient>();

            services.AddHttpClient(OpenLibraryClient.HttpClientName, client =>
            {
                client.BaseAddress = new Uri("https://openlibrary.org/");
                client.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        // Retry and wait: si la request falla por un error transitorio (5xx,
        // 408, o un problema de red), reintentamos hasta 3 veces con backoff
        // exponencial (2, 4, 8 segundos) en vez de fallar de una.
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );
        }

        // Circuit breaker: si tenemos 5 fallos seguidos, asumimos que Open
        // Library esta caido y dejamos de intentar por 30 segundos - asi no
        // seguimos golpeando (y esperando el timeout de) un servicio que ya
        // sabemos que no responde.
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}
