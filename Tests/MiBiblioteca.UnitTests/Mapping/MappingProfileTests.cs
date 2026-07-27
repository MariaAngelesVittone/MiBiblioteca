using AutoMapper;
using MiBiblioteca.Application.Mapping;
using Xunit;

namespace MiBiblioteca.UnitTests.Mapping
{
    // AssertConfigurationIsValid() revisa que TODAS las propiedades del
    // destino de cada CreateMap tengan de donde salir en el origen. Si
    // alguien agrega una propiedad a BookResponseDto y se olvida de
    // mapearla, este test la detecta en el momento, no en produccion
    // cuando alguien note que un campo siempre viene en null.
    public class MappingProfileTests
    {
        [Fact]
        public void Configuracion_DeMapeos_EsValida()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            configuration.AssertConfigurationIsValid();
        }
    }
}
