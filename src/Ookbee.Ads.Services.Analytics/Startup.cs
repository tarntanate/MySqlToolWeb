using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;

namespace Ookbee.Ads.Services.Analytics
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
            // Infrastructure
            services.AddInfrastructure(Configuration);

            // RDBMS
            services.AddDbContext<AnalyticsDbContext>();
            services.AddScoped(typeof(AnalyticsDbRepository<>));
        }

        public void Configure(IApplicationBuilder builder, IHostEnvironment envirinment)
        {
            builder.UseInfrastructure(envirinment);
        }
    }
}
