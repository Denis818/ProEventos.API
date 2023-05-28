using Microsoft.Extensions.DependencyInjection;
using Data.Intefaces;
using Data.Repository;
using System.Reflection;

namespace Application.Configurations.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependencyInjectios(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
