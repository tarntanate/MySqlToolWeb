using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Common.Helpers;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class InfrastructureMiddlewareExtensions
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IHostEnvironment envirinment)
        {
            HttpContextHelper.Configure(builder.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            if (envirinment.IsProduction())
            {
                builder.UseHttpsRedirection();
                builder.UseHsts();
            }
            else
            {
                builder.UseDeveloperExceptionPage();
            }
            builder.UseCors("AllowedHosts");
            builder.UseRouting();
            builder.UseAuthorization();
            builder.UseEndpoints(config =>
            {
                config.MapControllers();
                config.MapHealthChecks("/api/health");
            });
            return builder;
        }
    }
}
