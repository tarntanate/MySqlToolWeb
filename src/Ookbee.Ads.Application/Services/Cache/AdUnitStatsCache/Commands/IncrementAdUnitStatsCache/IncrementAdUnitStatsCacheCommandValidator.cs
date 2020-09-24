﻿using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache
{
    public class IncrementAdUnitStatsCacheCommandValidator : AbstractValidator<IncrementAdUnitStatsCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public IncrementAdUnitStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != AdStatsType.Request &&
                        value != AdStatsType.Fill)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                });
        }
    }
}
