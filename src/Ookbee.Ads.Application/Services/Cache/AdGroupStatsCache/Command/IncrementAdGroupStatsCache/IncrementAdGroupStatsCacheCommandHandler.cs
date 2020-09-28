using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommandHandler : IRequestHandler<IncrementAdGroupStatsCacheCommand, Response<bool>>
    {
        private readonly IDatabase AdsRedis;

        public IncrementAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(IncrementAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (!keyExists)
            {
                var hashField = request.StatsType.ToString();
                await AdsRedis.HashIncrementAsync(redisKey, hashField, 1, CommandFlags.FireAndForget);
                return new Response<bool>().OK(true);
            }
            return new Response<bool>().NotFound($"Unable to update stats: Invalid or expired data.");
        }
    }
}
