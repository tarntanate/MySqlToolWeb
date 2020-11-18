using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdByGroupIdCache;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdByGroupIdCache;
using Ookbee.Ads.Infrastructure;
using Ookbee.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tasks
{
    public class AdUnitIdByGroupIdCacheTask : ScheduledProcessor
    {
        public AdUnitIdByGroupIdCacheTask(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<AdGroupIdCacheTask> logger) : base(logger, serviceScopeFactory) { }

        protected override bool IsExecuteOnServerRestart => true;

        protected override string Schedule => GlobalVar.AppSettings.CronJobs.CacheGenerator;

        protected override async Task ProcessInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new CreateAdUnitIdByGroupIdCacheCommand(), stoppingToken);
        }
    }
}
