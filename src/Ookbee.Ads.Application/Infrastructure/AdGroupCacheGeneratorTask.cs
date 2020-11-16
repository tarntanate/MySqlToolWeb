using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Infrastructure;
using Ookbee.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class AdGroupCacheGeneratorTask : ScheduledProcessor
    {
        public AdGroupCacheGeneratorTask(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<AdGroupCacheGeneratorTask> logger) : base(logger, serviceScopeFactory) { }

        protected override bool IsExecuteOnServerRestart => true;

        protected override string Schedule => GlobalVar.AppSettings.CronJobs.CacheGenerator;

        protected override async Task ProcessInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            var caculatedAt = MechineDateTime.Date;
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new CreateAdGroupCacheCommand(caculatedAt), stoppingToken);
        }
    }
}
