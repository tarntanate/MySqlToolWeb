using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ookbee.Ads.Application.Extensions.Builder;
using Ookbee.Ads.Application.Extensions.DependencyInjection;
using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Services.Management
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            // services.AddHostedService<AdCacheService>();
            // services.AddHostedService<AdStatsCacheService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseInfrastructure(env);
        }
    }
}
