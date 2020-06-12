using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo.AdsMongo;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;

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
            // Infrastructure
            services.AddInfrastructure(Configuration);

            // RDBMS
            services.AddDbContext<AdsDbContext>();
            services.AddScoped(typeof(AdsDbRepository<>));

            // MongoDB
            services.AddSingleton<AdsMongoContext>();
            services.AddScoped(typeof(AdsMongoRepository<>));
            ConventionRegistry.Register("CamelCaseElementName", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            // Redis
            services.AddSingleton<AdsRedisContext>();
        }

        public void Configure(IApplicationBuilder builder, IHostEnvironment envirinment)
        {
            builder.UseInfrastructure(envirinment);
        }
    }
}

