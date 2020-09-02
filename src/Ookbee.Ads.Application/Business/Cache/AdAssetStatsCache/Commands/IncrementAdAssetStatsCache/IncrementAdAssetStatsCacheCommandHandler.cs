using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetsStatsCache.Commands.IncrementAdAssetsStatsCache
{
    public class IncrementAdAssetsStatsCacheCommandHandler : IRequestHandler<IncrementAdAssetsStatsCacheCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdAssetsStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(IncrementAdAssetsStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);

            return Unit.Value;
        }
    }
}
