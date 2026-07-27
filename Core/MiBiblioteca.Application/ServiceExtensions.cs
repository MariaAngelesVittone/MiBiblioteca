using Microsoft.Extensions.DependencyInjection;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Application.Mapping;
using MiBiblioteca.Application.Services;

namespace MiBiblioteca.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            return services;
        }
    }
}
