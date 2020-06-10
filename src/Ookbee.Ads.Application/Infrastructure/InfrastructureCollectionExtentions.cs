using AutoMapper;
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
using Ookbee.Ads.Persistence.Advertising.Mongo;
using Ookbee.Ads.Persistence.EFCore;
using System.Reflection;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class InfrastructureCollectionExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // RDBMS
            services.AddDbContext<AdsEFCoreContext>();
            services.AddScoped(typeof(AdsEFCoreRepository<>));

            // MongoDB
            services.AddSingleton<AdsMongoContext>();
            services.AddScoped(typeof(AdsMongoRepository<>));
            ConventionRegistry.Register("CamelCaseElementName", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            // Redis
            //services.AddSingleton<OokbeeAdsRedisContext>();

            // Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Health
            services.AddHealthChecks()
                    .AddMongoDb(configuration["AppSettings:ConnectionStrings:MongoDb"])
                    .AddNpgSql(configuration["AppSettings:ConnectionStrings:PostgreSQL"])
                    .AddRedis(configuration["AppSettings:ConnectionStrings:Redis"]);

            // Options
            services.AddAllowedHosts(configuration);
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

            // Configure
            services.Configure<ApiBehaviorOptions>((options) => options.SuppressModelStateInvalidFilter = true);
        }
    }
}
