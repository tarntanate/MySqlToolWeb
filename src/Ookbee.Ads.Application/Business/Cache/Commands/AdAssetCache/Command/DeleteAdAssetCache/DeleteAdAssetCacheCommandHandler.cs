using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
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
            var redisKey = CacheKey.Ad(request.AdId);
            await AdsRedis.KeyDeleteAsync(redisKey);

            return Unit.Value;
        }
    }
}
