using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommandHandler : IRequestHandler<IncrementAdUnitStatsCacheCommand, Response<bool>>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdUnitStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(IncrementAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {

            var redisKey = CacheKey.UnitsStats(request.AdUnitId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (!keyExists)
            {
                var hashField = request.StatsType.ToString();
                await AdsRedis.HashIncrementAsync(redisKey, hashField, 1, CommandFlags.FireAndForget);
                return new Response<bool>().Success(true);
            }
            return new Response<bool>().Fail(404, $"Unable to update stats: Invalid or expired data.");
        }
    }
}
