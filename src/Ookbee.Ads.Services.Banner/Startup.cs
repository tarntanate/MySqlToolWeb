using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Services.Banner
{
    
public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder builder, IHostEnvironment envirinment)
        {
            builder.UseInfrastructure(envirinment);
        }
    }
}
