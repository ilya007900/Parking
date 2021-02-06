using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shared.Infrastructure
{
    public static class SwaggerExtensions
    {
        public const string Version = "v1";

        public static IServiceCollection AddOpenApi(this IServiceCollection services, string version = Version)
        {
            services.AddSwaggerGen(opt => opt.SwaggerDoc(version, new OpenApiInfo
            {
                Version = version
            }));

            return services;
        }

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, string applicationName,
            string version = Version)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", applicationName);
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}