using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Common.AspNetCore.Middlewares;
using Ookbee.Ads.Common.Swagger;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Extensions.Builder
{
    public static class AppMiddlewareExtension
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (GlobalVar.Services == null)
                GlobalVar.Services = app.ApplicationServices;

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }
            else
            {
                app.UseHttpExceptionMiddleware();
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
