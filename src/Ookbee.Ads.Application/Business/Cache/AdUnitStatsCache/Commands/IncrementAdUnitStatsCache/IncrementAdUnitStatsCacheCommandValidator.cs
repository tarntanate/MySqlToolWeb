﻿using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommandValidator : AbstractValidator<IncrementAdUnitStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdUnitStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdUnitId, p.Platform })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.UnitsStats(value.AdUnitId, value.Platform);
                    var redisValue = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!redisValue.HasValue())
                        context.AddFailure($"AdUnitStats '{redisKey}' doesn't exist.");
                });
        }
    }
}
