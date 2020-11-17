using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupRedis.Commands.UpdateAdGroupStatsRedis
{
    public class UpdateAdGroupStatsRedisCommandHandler : IRequestHandler<UpdateAdGroupStatsRedisCommand, Response<bool>>
    {
        private readonly IDatabase AdsRedis;

        public UpdateAdGroupStatsRedisCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(UpdateAdGroupStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var redisKey = RedisKeys.GroupStats(request.AdGroupId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (keyExists)
            {
                var hashField = request.StatsType.ToString();
                var hashValue = request.Value;
                await AdsRedis.HashIncrementAsync(redisKey, hashField, hashValue, CommandFlags.FireAndForget);
                return response.OK(true);
            }
            return response.NotFound($"Unable to update stats: Invalid or expired data.");
        }
    }
}
