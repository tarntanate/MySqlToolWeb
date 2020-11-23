using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitByPlatformCache;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAvailableAdUnitCache;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteUnavailableAdUnitCache;
using Ookbee.Ads.Infrastructure;
using Ookbee.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tasks
{
    public class AdUnitCachingTask : ScheduledProcessor
    {
        public AdUnitCachingTask(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<AdGroupCachingTask> logger) : base(logger, serviceScopeFactory) { }

        protected override bool IsExecuteOnServerRestart => true;

        protected override string Schedule => GlobalVar.AppSettings.CronJobs.CacheGenerator;

        protected override async Task ProcessInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new DeleteUnavailableAdUnitCacheCommand(), stoppingToken);
            await mediator.Send(new CreateAvailableAdUnitCacheCommand(), stoppingToken);
            await mediator.Send(new CreateAdUnitByPlatformCacheCommand(), stoppingToken);
        }
    }
}
