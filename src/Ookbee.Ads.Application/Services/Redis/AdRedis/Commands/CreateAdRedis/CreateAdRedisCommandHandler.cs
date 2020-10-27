using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdRevealList;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdByPlatformRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdStatsRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdRedis
{
    public class CreateAdRedisCommandHandler : IRequestHandler<CreateAdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var getAdList = await Mediator.Send(new GetAdRevealListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdList.IsSuccess)
                {
                    var ads = getAdList.Data;
                    foreach (var ad in ads)
                    {
                        var getAdQuota = await Mediator.Send(new GetAdStatsQuery(request.CaculatedAt, ad.Id), cancellationToken);
                        if (getAdQuota.IsSuccess && 
                            getAdQuota.Data.Quota > getAdQuota.Data.Impression)
                        {
                            await Mediator.Send(new CreateAdIdRedisCommand(ad.Id));
                            await Mediator.Send(new CreateAdByPlatformRedisCommand(ad.Id));
                            await Mediator.Send(new CreateAdStatsRedisCommand(request.CaculatedAt, ad.Id));
                        }
                    }
                    next = ads.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
