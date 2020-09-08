using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsAllCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CacheArchiveStatsService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CacheArchiveStatsService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var next = true;
                    do
                    {
                        var caculatedAt = MechineDateTime.Now.Date;
                        await mediator.Send(new ArchiveAdGroupStatsAllCacheCommand(caculatedAt), cancellationToken);
                        var nowDateTime = MechineDateTime.Now;
                        var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromDays(1));
                        var timeout = nextDateTime - nowDateTime;
                        Thread.Sleep(timeout);
                    }
                    while (next);
                }
            });
        }
    }
}
