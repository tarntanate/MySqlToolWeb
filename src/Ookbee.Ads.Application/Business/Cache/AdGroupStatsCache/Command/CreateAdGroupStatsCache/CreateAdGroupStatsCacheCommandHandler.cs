using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatCache
{
    public class CreateAdGroupStatsCacheCommandHandler : IRequestHandler<CreateAdGroupStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public CreateAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupStats(request.AdGroupId, request.Platform);
            var hashField = request.Stats.ToString();
            var hashValue = request.Value;
            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            return Unit.Value;
        }
    }
}
