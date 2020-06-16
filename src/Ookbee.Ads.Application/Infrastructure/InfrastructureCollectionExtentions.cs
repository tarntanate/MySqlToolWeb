using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ookbee.Ads.Common.AspNetCore.Attributes;
using Ookbee.Ads.Common.AspNetCore.Extentions;
using Ookbee.Ads.Common.AspNetCore.OutputFormatters;
using Ookbee.Ads.Common.Swagger;
using Ookbee.Ads.Infrastructure;
using System;
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
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

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
