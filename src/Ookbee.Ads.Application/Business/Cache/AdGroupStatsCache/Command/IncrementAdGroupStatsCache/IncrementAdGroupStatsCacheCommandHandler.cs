using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommandHandler : IRequestHandler<IncrementAdGroupStatsCacheCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(IncrementAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupStats(request.AdGroupId, request.Platform);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);
            return Unit.Value;
        }
    }
}
