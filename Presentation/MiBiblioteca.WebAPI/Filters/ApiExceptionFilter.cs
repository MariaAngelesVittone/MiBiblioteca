using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiBiblioteca.WebAPI.Filters
{
    // Este filtro es la red de seguridad GLOBAL para excepciones que nadie
    // atrapo explicitamente. Es distinto del try/catch que ya tiene
    // AuthController: ese try/catch atrapa errores de NEGOCIO esperados
    // (ej. "el usuario ya existe") y devuelve un mensaje puntual. Este
    // filtro atrapa lo inesperado (una NullReferenceException, un timeout,
    // etc.) para que el cliente nunca reciba la pagina de stack trace
    // cruda de ASP.NET, sino un JSON prolijo con 500.
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Excepcion no manejada en {Action}",
                context.ActionDescriptor.DisplayName);

            context.Result = new ObjectResult(new
            {
                error = "Ocurrio un error inesperado en el servidor.",
                detail = context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
