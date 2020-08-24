using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetworkStats.Commands.UpdateGroupStats
{
    public class UpdateGroupStatsCommandHandler : IRequestHandler<UpdateGroupStatsCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public UpdateGroupStatsCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitStatsByGroup(request.AdGroupId);
            var hashField = request.Stats.ToString();
            Console.WriteLine(hashField);
            await AdsRedis.HashIncrementAsync(redisKey, hashField);
            return Unit.Value;
        }
    }
}
