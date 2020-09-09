﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommandHandler : IRequestHandler<IncrementAdStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(IncrementAdStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId, request.Platform);
            var hashField = request.StatsType.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField, 1, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
