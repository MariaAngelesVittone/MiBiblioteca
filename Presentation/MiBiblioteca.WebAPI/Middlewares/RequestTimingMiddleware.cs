using System.Diagnostics;

namespace MiBiblioteca.WebAPI.Middlewares
{
    // Un MIDDLEWARE opera en el pipeline crudo de HTTP: no sabe nada de
    // controllers, actions ni de MVC. Recibe un HttpContext y decide si
    // sigue la cadena (llamando a "next") o corta ahi. Por eso un
    // middleware puede medir TODO lo que pasa para una request -incluida
    // la autenticacion y el ruteo- mientras que un filtro (ver Filters/)
    // solo ve lo que pasa dentro de la ejecucion de una action de MVC.
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // No podemos setear el header despues de "await _next(context)":
            // para respuestas sin Content-Length (chunked), Kestrel puede
            // empezar a enviar los headers de la response ANTES de que el
            // resto del pipeline termine, y en ese punto los headers ya son
            // de solo lectura. OnStarting registra un callback que corre
            // justo antes de que salga el primer byte, sin importar cuando
            // sea eso, asi que siempre llega a tiempo.
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Response-Time-ms"] = stopwatch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation(
                "{Method} {Path} respondio {StatusCode} en {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
