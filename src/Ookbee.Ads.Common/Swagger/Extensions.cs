using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ookbee.Ads.Common.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            var options = new SwaggerOptions();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<SwaggerOptions>(configuration.GetSection("swagger"));
                configuration.GetSection("swagger").Bind(options);
            }

            if (!options.Enabled)
                return services;

            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = new SwaggerOptions();

            var x = builder
                .ApplicationServices
                .GetService<IConfiguration>();
            builder
                .ApplicationServices
                .GetService<IConfiguration>()
                .GetSection("Swagger")
                .Bind(options);

            if (!options.Enabled)
                return builder;

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder
                .UseStaticFiles()
                .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return options.ReDocEnabled
                ? builder.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SpecUrl = $"{options.Name}/swagger.json";
                })
                : builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    c.RoutePrefix = routePrefix;
                });
        }
    }
}