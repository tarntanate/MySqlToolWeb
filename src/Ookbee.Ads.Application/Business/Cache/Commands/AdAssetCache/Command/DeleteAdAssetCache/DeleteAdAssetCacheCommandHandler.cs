using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCache
{
    public class DeleteAdAssetCacheCommandHandler : IRequestHandler<DeleteAdAssetCacheCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteAdAssetCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteAdAssetCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                var redisKey = CacheKey.Ad(request.AdId);
                var redisValue = request.AdId;
                var platforms = Enum.GetValues(typeof(Platform)).Cast<string>();
                await AdsRedis.KeyDeleteAsync(redisKey);
                foreach (var platform in platforms)
                {
                    redisKey = CacheKey.AdIdsByUnit(getAdById.Data.AdUnit.Id, platform);
                    await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                }
            }

            return Unit.Value;
        }
    }
}
