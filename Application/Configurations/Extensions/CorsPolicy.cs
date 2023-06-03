using Microsoft.AspNetCore.Builder;

namespace Application.Configurations.Extensions
{
    public static class CorsPolicy
    {
        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        }
    }
}
