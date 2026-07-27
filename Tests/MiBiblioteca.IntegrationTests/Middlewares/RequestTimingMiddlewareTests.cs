using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MiBiblioteca.WebAPI.Middlewares;

namespace MiBiblioteca.IntegrationTests.Middlewares
{
    // Este es un test de INTEGRACION, no unitario: usamos TestServer para
    // levantar un pipeline HTTP real (no un HttpContext armado a mano). Es
    // necesario porque el bug real que encontramos al correr la app de
    // verdad -"Headers are read-only, response has already started"- solo
    // aparece cuando el servidor empieza a mandar la respuesta ANTES de que
    // el middleware termine de ejecutar el resto del pipeline. Un
    // HttpContext de prueba armado a mano no dispara ese comportamiento.
    public class RequestTimingMiddlewareTests
    {
        [Fact]
        public async Task Pipeline_ConRequestTimingMiddleware_AgregaHeaderAunConRespuestaYaIniciada()
        {
            // Arrange: un endpoint terminal que escribe al body SIN fijar
            // Content-Length, para forzar que la respuesta se empiece a
            // enviar durante la escritura (como pasaba con el JSON de
            // BooksController), y no recien al final del pipeline.
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.Configure(app =>
                    {
                        app.UseMiddleware<RequestTimingMiddleware>();
                        app.Run(async context =>
                        {
                            await context.Response.StartAsync();
                            await context.Response.WriteAsync("contenido de prueba");
                        });
                    });
                })
                .StartAsync();

            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            Assert.True(response.Headers.Contains("X-Response-Time-ms"));
            var valorHeader = response.Headers.GetValues("X-Response-Time-ms").Single();
            Assert.True(long.TryParse(valorHeader, out var ms));
            Assert.True(ms >= 0);
        }
    }
}
