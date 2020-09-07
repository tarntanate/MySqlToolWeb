﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsCache
{
    public class CreateAdStatsCacheCommandHandler : IRequestHandler<CreateAdStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public CreateAdStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId, request.Platform);
            var hashField = request.Stats.ToString();
            var hashValue = request.Value;
            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            return Unit.Value;
        }
    }
}
