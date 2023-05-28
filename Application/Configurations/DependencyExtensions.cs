using Microsoft.Extensions.DependencyInjection;
using Data.Intefaces;
using Data.Repository;

namespace Application.Configurations
{
    public static class DependencyExtensions
    {
        public static void AddDependencyInjectios(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
