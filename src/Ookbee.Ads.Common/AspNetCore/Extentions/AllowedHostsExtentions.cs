using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Common.AspNetCore.Extentions
{
    public static class AllowedHostsExtentions
    {
        public static void AddAllowedHosts(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedHosts = configuration.GetValue<string>("AllowedHosts");
            if (allowedHosts.HasValue())
            {
                var corsPolicyBuilder = new CorsPolicyBuilder();
                if (allowedHosts == "*")
                {
                    corsPolicyBuilder.SetIsOriginAllowed(_ => true);
                }
                else
                {
                    corsPolicyBuilder.WithOrigins(allowedHosts.Split(";"));
                }
                services.AddCors(options =>
                {
                    options.AddPolicy(
                        name: "AllowSpecificOrigins",
                        policy: corsPolicyBuilder.AllowAnyHeader()
                                                 .AllowAnyMethod()
                                                 .AllowCredentials()
                                                 .Build()
                    );
                });
            }
        }
    }
}
