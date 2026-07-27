using System.Net;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Xunit;

namespace MiBiblioteca.UnitTests.Resilience
{
    // Estos tests prueban las politicas de resiliencia EN AISLAMIENTO, sin
    // levantar ningun servidor ni depender de que una API externa este
    // caida (cosa que no podemos controlar ni simular de forma confiable
    // contra la Open Library real). Probamos directamente el comportamiento
    // que Polly nos da: reintentar, y cortar el circuito.
    public class PollyPolicyTests
    {
        [Fact]
        public async Task RetryPolicy_ConDosFallosTransitorios_TerminaExitosoAlTercerIntento()
        {
            // Arrange: la misma politica que usamos en ExternalServices,
            // pero sin esperar tiempo real entre reintentos.
            var intentos = 0;
            var policy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: _ => TimeSpan.Zero);

            // Act: simulamos un servicio que falla las primeras 2 veces
            // (500 Internal Server Error, un "error transitorio") y a la
            // tercera responde bien.
            var resultado = await policy.ExecuteAsync(() =>
            {
                intentos++;
                var status = intentos < 3 ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
                return Task.FromResult(new HttpResponseMessage(status));
            });

            // Assert
            Assert.Equal(3, intentos);
            Assert.Equal(HttpStatusCode.OK, resultado.StatusCode);
        }

        [Fact]
        public async Task CircuitBreakerPolicy_TrasLosFallosPermitidos_SeAbreYCortaSinEjecutar()
        {
            // Arrange: se abre el circuito despues de 2 fallos seguidos.
            var policy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromSeconds(30));

            // Act: provocamos los 2 fallos que abren el circuito.
            for (var i = 0; i < 2; i++)
            {
                await policy.ExecuteAsync(() =>
                    Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)));
            }

            // El circuito ya deberia estar abierto. La proxima llamada tiene
            // que fallar de inmediato con BrokenCircuitException, SIN llegar
            // a ejecutar el delegado (por eso "ejecutado" tiene que quedar
            // en false).
            var ejecutado = false;

            // Assert: como la politica es generica sobre HttpResponseMessage,
            // la excepcion que tira tambien lo es (BrokenCircuitException<T>,
            // no la clase base BrokenCircuitException a secas).
            await Assert.ThrowsAsync<BrokenCircuitException<HttpResponseMessage>>(() => policy.ExecuteAsync(() =>
            {
                ejecutado = true;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }));

            Assert.False(ejecutado);
        }
    }
}
