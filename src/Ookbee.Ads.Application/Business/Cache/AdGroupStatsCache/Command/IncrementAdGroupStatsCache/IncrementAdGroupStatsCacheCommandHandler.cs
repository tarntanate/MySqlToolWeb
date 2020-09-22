using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommandHandler : IRequestHandler<IncrementAdGroupStatsCacheCommand, HttpResult<bool>>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<bool>> Handle(IncrementAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (!keyExists)
            {
                var hashField = request.StatsType.ToString();
                await AdsRedis.HashIncrementAsync(redisKey, hashField, 1, CommandFlags.FireAndForget);
                return new HttpResult<bool>().Success(true);
            }
            return new HttpResult<bool>().Fail(404, $"Unable to update stats: Invalid or expired data.");
        }
    }
}
