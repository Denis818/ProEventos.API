using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System.Text.Json.Serialization;

namespace ProEventos.API.Configuration.Extensions.Dependencies
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

        public static void AddCustomSeriLog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm} {Level:u3}] {Message:lj}{NewLine}{Exception}\n",
                theme: AnsiConsoleTheme.Sixteen)
            .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
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
