﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.DeleteAdGroupStatsCache
{
    public class DeleteAdGroupStatsCacheCommandHandler : IRequestHandler<DeleteAdGroupStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public DeleteAdGroupStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            await AdsRedis.KeyDeleteAsync(redisKey);
            return Unit.Value;
        }
    }
}