using Microsoft.Extensions.DependencyInjection;
using Data.Intefaces;
using Data.Repository;
using System.Reflection;
using Data.Interfaces;

namespace Application.Configurations.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IPalestrantesRepository, PalestrantesRepository>();
            services.AddScoped<IRedeSocialRepository, RedeSocialRepository>();
        }
    }
}
