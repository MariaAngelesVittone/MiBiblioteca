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
    public class ApiExceptionFilterTests
    {
        [Fact]
        public void OnException_ConvierteLaExcepcionEnUnObjectResult500()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ApiExceptionFilter>>();
            var sut = new ApiExceptionFilter(loggerMock.Object);

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor { DisplayName = "AccionQueFalla" });

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new InvalidOperationException("algo salio mal")
            };

            // Act
            sut.OnException(exceptionContext);

            // Assert: el filtro debe marcar la excepcion como manejada y
            // devolver un 500 prolijo en vez de dejar que explote el pipeline.
            Assert.True(exceptionContext.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(exceptionContext.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
