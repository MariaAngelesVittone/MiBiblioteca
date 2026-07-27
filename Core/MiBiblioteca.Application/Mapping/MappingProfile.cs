using AutoMapper;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // OJO con esto: AutoMapper (CVE-2026-32933) puede colgarse con un
            // StackOverflow si mapeamos un tipo que se referencia a si mismo
            // (por ejemplo Book -> BookDto -> ReadingEntryDto -> BookDto...)
            // sin poner un limite. Como BookResponseDto NO incluye
            // ReadingEntries, este mapeo es plano y no tiene ese riesgo.
            CreateMap<Book, BookResponseDto>();

            // Book tiene propiedades (Id, las fechas de BaseEntity,
            // ReadingEntries) que CreateBookDto no manda a proposito - las
            // pone el propio sistema, no el cliente. Sin este .ForMember
            // explicito, AssertConfigurationIsValid() falla: por defecto
            // AutoMapper exige que cada propiedad del destino tenga de
            // donde salir en el origen.
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ReadingEntries, opt => opt.Ignore());
        }
    }
}
