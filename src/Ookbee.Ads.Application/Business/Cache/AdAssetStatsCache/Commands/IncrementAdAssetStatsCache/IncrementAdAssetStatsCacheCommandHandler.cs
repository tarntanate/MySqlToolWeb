using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.IncrementAdAssetStatsCache
{
    public class IncrementAdAssetStatsCacheCommandHandler : IRequestHandler<IncrementAdAssetStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdAssetStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(IncrementAdAssetStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId, request.Platform);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);

            return Unit.Value;
        }
    }
}
