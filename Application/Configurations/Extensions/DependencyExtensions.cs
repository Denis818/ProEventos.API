using Microsoft.Extensions.DependencyInjection;
using Data.Intefaces;
using Data.Repository;
using System.Reflection;
using Data.Interfaces;
using Application.Interfaces.Utility;
using Application.Utilities;
using Application.Services;
using Application.Interfaces.Services;

namespace Application.Configurations.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IPalestrantesRepository, PalestrantesRepository>();
            services.AddScoped<IRedeSocialRepository, RedeSocialRepository>();

            services.AddScoped<IEventoService, EventoService>();

            services.AddScoped<INotificador, Notificador>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
