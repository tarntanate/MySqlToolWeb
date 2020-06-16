using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Ookbee.Ads.Common.AspNetCore.Attributes;
using Ookbee.Ads.Common.AspNetCore.Extentions;
using Ookbee.Ads.Common.AspNetCore.OutputFormatters;
using Ookbee.Ads.Infrastructure;
using System.Globalization;
using System.Reflection;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class InfrastructureCollectionExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Health
            services.AddHealthChecks()
                    .AddMongoDb(configuration[GlobalVar.AppSettings.ConnectionStrings.MongoDB.Ads])
                    .AddNpgSql(configuration[GlobalVar.AppSettings.ConnectionStrings.PostgreSQL.Ads])
                    .AddRedis(configuration[GlobalVar.AppSettings.ConnectionStrings.Redis]);

            // Options
            services.AddAllowedHosts(configuration);
            services.AddHttpContextAccessor();
            services.AddControllers((options) =>
                    {
                        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                        options.OutputFormatters.Insert(0, new ApiOutputFormatter());
                    })
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.Configure<ApiBehaviorOptions>((options) => options.SuppressModelStateInvalidFilter = true);
        }
    }
}
