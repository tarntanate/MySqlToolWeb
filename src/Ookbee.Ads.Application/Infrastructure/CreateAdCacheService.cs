using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CreateAdCacheService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CreateAdCacheService(
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
                        await mediator.Send(new DeleteAdGroupRedisCommand(), cancellationToken);
                        await mediator.Send(new CreateAdGroupRedisCommand(caculatedAt), cancellationToken);
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
