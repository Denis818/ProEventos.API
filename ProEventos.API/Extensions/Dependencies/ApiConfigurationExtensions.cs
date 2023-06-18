using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;

namespace ProEventos.API.Extensions.Dependencies
{
    public static class ApiConfigurationExtensions
    {
        public static void AddControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        }
        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        }
    }
}
