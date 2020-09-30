﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.DeleteAdUnitStatsCache
{
    public class DeleteAdUnitStatsCacheCommandHandler : IRequestHandler<DeleteAdUnitStatsCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdUnitStatsCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitStats(request.AdUnitId);
            await AdsRedis.KeyDeleteAsync(redisKey);

            return Unit.Value;
        }
    }
}
