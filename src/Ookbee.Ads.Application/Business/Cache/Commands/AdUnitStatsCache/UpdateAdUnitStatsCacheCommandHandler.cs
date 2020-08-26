﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdUnitStatsCache
{
    public class UpdateAdUnitStatsCacheCommandHandler : IRequestHandler<UpdateAdUnitStatsCacheCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public UpdateAdUnitStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitsStats(request.AdUnitId);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);

            return Unit.Value;
        }
    }
}