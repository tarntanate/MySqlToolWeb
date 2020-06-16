using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json.Converters;
using Ookbee.Ads.Common.AspNetCore.Attributes;
using Ookbee.Ads.Common.AspNetCore.Extentions;
using Ookbee.Ads.Common.AspNetCore.OutputFormatters;
using Ookbee.Ads.Common.Swagger;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo.AdsMongo;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using System.Reflection;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class InfrastructureCollectionExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // MVC
            services.Configure<ApiBehaviorOptions>((options) => options.SuppressModelStateInvalidFilter = true);
            services.AddHttpContextAccessor();
            services.AddControllers((options) =>
                    {
                        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                        options.OutputFormatters.Insert(0, new ApiOutputFormatter());
                    })
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                    .AddNewtonsoftJson((options) =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            // CORS
            services.AddAllowedHosts(configuration);

            // Health
            var connMongoDB = GlobalVar.AppSettings.ConnectionStrings.MongoDB.Ads;
            var connPostgreSQL = GlobalVar.AppSettings.ConnectionStrings.PostgreSQL.Ads;
            var connRedis = GlobalVar.AppSettings.ConnectionStrings.Redis;
            services.AddHealthChecks()
                    .AddMongoDb(connMongoDB)
                    .AddNpgSql(connPostgreSQL)
                    .AddRedis(connRedis);

            // EFCore
            services.AddDbContext<AdsDbContext>();
            services.AddDbContext<AnalyticsDbContext>();
            services.AddScoped(typeof(AdsDbRepository<>));
            services.AddScoped(typeof(AnalyticsDbRepository<>));

            // MongoDB
            services.AddSingleton<AdsMongoContext>();
            services.AddScoped(typeof(AdsMongoRepository<>));
            ConventionRegistry.Register("CamelCaseElementName", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            // Redis
            services.AddSingleton<AdsRedisContext>();

            // Swagger
            services.AddSwaggerDocs();

            // Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Services
            services.AddSingleton<IConfiguration>(configuration);
        }
    }
}
