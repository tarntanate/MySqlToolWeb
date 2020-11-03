using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.ArchiveAdGroupStatsRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.ArchiveAdUnitStatsRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class AdStatsCacheService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public AdStatsCacheService(
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
                        var caculatedAt = MechineDateTime.Date;

                        await mediator.Send(new ArchiveAdGroupStatsRedisCommand(caculatedAt), cancellationToken);

                        await mediator.Send(new ArchiveAdUnitStatsRedisCommand(caculatedAt), cancellationToken);

                        await mediator.Send(new ArchiveAdStatsRedisCommand(caculatedAt), cancellationToken);

                        var nowDateTime = MechineDateTime.Now;
                        var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromSeconds(5));
                        var timeout = nextDateTime - nowDateTime;
                        Thread.Sleep(timeout);
                    }
                    while (next);
                }
            });
        }
    }
}
