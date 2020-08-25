using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdGroupStatsCache
{
    public class UpdateAdGroupStatsCacheCommandHandler : IRequestHandler<UpdateAdGroupStatsCacheCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public UpdateAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(UpdateAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitStatsByGroup(request.AdGroupId);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);
            return Unit.Value;
        }
    }
}
