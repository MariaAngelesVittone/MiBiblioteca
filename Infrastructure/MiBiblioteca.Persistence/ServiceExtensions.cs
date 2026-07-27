using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MiBibliotecaContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("MiBibliotecaDb")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
