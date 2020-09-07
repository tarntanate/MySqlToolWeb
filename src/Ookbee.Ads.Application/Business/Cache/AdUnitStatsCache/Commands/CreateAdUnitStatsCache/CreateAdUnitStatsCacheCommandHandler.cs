﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache
{
    public class CreateAdUnitStatsCacheCommandHandler : IRequestHandler<CreateAdUnitStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public CreateAdUnitStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitsStats(request.AdUnitId, request.Platform);
            var hashField = request.Stats.ToString();
            var hashValue = request.Value;
            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            return Unit.Value;
        }
    }
}
