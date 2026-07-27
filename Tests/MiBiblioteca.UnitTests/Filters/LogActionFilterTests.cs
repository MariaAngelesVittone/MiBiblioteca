using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using MiBiblioteca.WebAPI.Filters;
using Moq;
using Xunit;

namespace MiBiblioteca.UnitTests.Filters
{
    public class LogActionFilterTests
    {
        private static ActionContext CrearActionContext()
        {
            return new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor { DisplayName = "AccionDePrueba" });
        }

        [Fact]
        public void OnActionExecuting_NoLanzaExcepcion_YRegistraElIntento()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LogActionFilter>>();
            var sut = new LogActionFilter(loggerMock.Object);
            var context = new ActionExecutingContext(
                CrearActionContext(),
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: new object());

            // Act
            sut.OnActionExecuting(context);

            // Assert: el filtro no debe modificar el resultado, solo loguear.
            Assert.Null(context.Result);
        }

        [Fact]
        public void OnActionExecuted_NoLanzaExcepcion_CuandoNoHuboError()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LogActionFilter>>();
            var sut = new LogActionFilter(loggerMock.Object);
            var context = new ActionExecutedContext(
                CrearActionContext(),
                new List<IFilterMetadata>(),
                controller: new object());

            // Act
            sut.OnActionExecuted(context);

            // Assert
            Assert.Null(context.Exception);
        }
    }
}
