using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Swagger;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class InfrastructureMiddlewareExtensions
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var x = app.ApplicationServices.GetService<IConfiguration>();
            HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowSpecificOrigins");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(config =>
            {
                config.MapControllers();
                config.MapHealthChecks("/api/health");
            });
            app.UseSwaggerDocs();

            return app;
        }
    }
}
