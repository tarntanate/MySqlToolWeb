using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Ookbee.Ads.Common.AspNetCore.Attributes;
using Ookbee.Ads.Common.AspNetCore.Extentions;
using Ookbee.Ads.Common.AspNetCore.OutputFormatters;
using Ookbee.Ads.Common.Swagger;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Advertising.Mongo.AdsMongo;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using System.Reflection;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class AppServiceCollectionExtention
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Config
            var appsettings = new AppSettings();
            configuration.GetSection(nameof(AppSettings)).Bind(appsettings);
            services.AddHttpContextAccessor();
            services.AddSingleton<IConfiguration>(configuration);
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.Configure<ApiBehaviorOptions>((options) => options.SuppressModelStateInvalidFilter = true);

            // MVC
            services.AddControllers((options) =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.OutputFormatters.Insert(0, new ApiOutputFormatter());
            })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddNewtonsoftJson((options) =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // CORS
            services.AddAllowedHosts(configuration);

            // Health
            services.AddHealthChecks()
                .AddMongoDb(appsettings.ConnectionStrings.MongoDB.Ads)
                .AddNpgSql(appsettings.ConnectionStrings.PostgreSQL.Ads)
                .AddRedis(appsettings.ConnectionStrings.Redis.Ads);

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

            // AutoMapper
            services.AddAutoMapper(cfg => { cfg.AllowNullCollections = true; }, Assembly.GetExecutingAssembly());

            // Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
