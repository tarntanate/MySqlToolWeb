using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommandHandler : IRequestHandler<IncrementAdUnitStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdUnitStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(IncrementAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitsStats(request.AdUnitId, request.Platform);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);

            return Unit.Value;
        }
    }
}
