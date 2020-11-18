using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdCache;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdCache;
using Ookbee.Ads.Infrastructure;
using Ookbee.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tasks
{
    public class AdUnitIdCacheTask : ScheduledProcessor
    {
        public AdUnitIdCacheTask(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<AdGroupIdCacheTask> logger) : base(logger, serviceScopeFactory) { }

        protected override bool IsExecuteOnServerRestart => true;

        protected override string Schedule => GlobalVar.AppSettings.CronJobs.CacheGenerator;

        protected override async Task ProcessInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new DeleteAdUnitIdCacheCommand(), stoppingToken);
            await mediator.Send(new CreateAdUnitIdCacheCommand(), stoppingToken);
        }
    }
}
