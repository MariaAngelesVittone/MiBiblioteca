using Microsoft.AspNetCore.Mvc.Filters;

namespace MiBiblioteca.WebAPI.Filters
{
    // Un FILTRO vive dentro del pipeline de MVC, no del pipeline crudo de
    // HTTP: se ejecuta justo antes/despues de que el framework invoque la
    // action de un controller, y por eso conoce cosas que un middleware no
    // conoce de forma directa (que action se va a ejecutar, con que
    // argumentos ya bindeados, que controller es). Un IActionFilter tiene
    // dos ganchos: OnActionExecuting (antes) y OnActionExecuted (despues).
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(
                "Entrando a {Action} con argumentos: {@Arguments}",
                context.ActionDescriptor.DisplayName,
                context.ActionArguments);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation(
                "Saliendo de {Action}, exception: {HasException}",
                context.ActionDescriptor.DisplayName,
                context.Exception is not null);
        }
    }
}
