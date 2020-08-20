using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheGroupStats
{
    public class CreateCacheGroupStatsCommandHandler : IRequestHandler<CreateCacheGroupStatsCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public CreateCacheGroupStatsCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateCacheGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitStatsByGroup(request.AdGroupId);
            var hashField = request.Stats.ToString();
            Console.WriteLine(hashField);
            await AdsRedis.HashIncrementAsync(redisKey, hashField);
            return Unit.Value;
        }
    }
}
