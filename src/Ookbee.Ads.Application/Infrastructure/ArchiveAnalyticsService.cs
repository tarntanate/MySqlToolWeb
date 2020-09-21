using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Application.Business.Cache.AdGroupStats.Commands.ArchiveAdGroupStats;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class ArchiveAnalyticsService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public ArchiveAnalyticsService(
            IServiceProvider serviceProvider)
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
                        try
                        {
                            var caculatedAt = MechineDateTime.Date;
                            Console.WriteLine(caculatedAt);
                            await mediator.Send(new ArchiveAdGroupStatsCommand(caculatedAt), cancellationToken);
                            var nowDateTime = MechineDateTime.Now;
                            var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromSeconds(3));
                            var timeout = nextDateTime - nowDateTime;
                            Console.WriteLine(timeout);
                            Thread.Sleep(timeout);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Thread.Sleep(50000);
                        }
                    }
                    while (next);
                }
            });
        }
    }
}
