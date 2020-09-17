using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStatsCache.Commands.InitialAdGroupStats;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class InitialAnalyticsService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public InitialAnalyticsService(IServiceProvider serviceProvider)
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
                        var caculatedAt = MechineDateTime.UtcNow.Date;
                        Console.WriteLine("MechineDateTime.Now: " + MechineDateTime.Now);
                        Console.WriteLine("MechineDateTime.Now.Date: " + MechineDateTime.Now.Date);
                        Console.WriteLine("MechineDateTime.UtcNow.Date: " + MechineDateTime.UtcNow.Date);
                        Console.WriteLine("MechineDateTime.WindowsTimeZoneId: " + MechineDateTime.WindowsTimeZoneId);
                        await mediator.Send(new InitialAdGroupStatsCommand(caculatedAt), cancellationToken);
                        var nowDateTime = MechineDateTime.UtcNow;
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
