using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class InitialAnalyticsCacheService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public InitialAnalyticsCacheService(IServiceProvider serviceProvider)
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
                        await mediator.Send(new InitialAdGroupStatsCacheCommand(caculatedAt), cancellationToken);
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
