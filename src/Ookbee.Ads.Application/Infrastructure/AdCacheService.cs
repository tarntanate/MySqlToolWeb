using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupByPublisherRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupByPublisherRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUserPreviewRedis;
using Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.CreateAdUserPreviewRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class AdCacheService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public AdCacheService(
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

                            // await mediator.Send(new DeleteAdGroupByPublisherRedisCommand(), cancellationToken);
                            // await mediator.Send(new CreateAdGroupByPublisherRedisCommand(), cancellationToken);

                            // await mediator.Send(new DeleteAdGroupRedisCommand(caculatedAt), cancellationToken);
                            // await mediator.Send(new CreateAdGroupRedisCommand(caculatedAt), cancellationToken);

                            // await mediator.Send(new DeleteAdUserPreviewRedisCommand(), cancellationToken);
                            // await mediator.Send(new CreateAdUserPreviewRedisCommand(), cancellationToken);

                            var nowDateTime = MechineDateTime.Now;
                            var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromSeconds(5));
                            var timeout = nextDateTime - nowDateTime;
                            Thread.Sleep(timeout);
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(5));
                        }
                    }
                    while (next);
                }
            });
        }
    }
}
