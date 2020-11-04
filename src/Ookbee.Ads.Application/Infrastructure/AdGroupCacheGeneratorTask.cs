using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
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
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var getAdList = await mediator.Send(new GetAdListQuery(0, 100, null, null), stoppingToken);
        }
    }
}
