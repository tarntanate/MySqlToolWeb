using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCacheByPlatform
{
    public class IncrementAdStatsCacheByPlatformCommandValidator : AbstractValidator<IncrementAdStatsCacheByPlatformCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdStatsCacheByPlatformCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdId, p.Platform, p.StatsType })
                .Custom((value, context) =>
                {
                    if (value.Platform == Platform.Unknown)
                    {
                        context.AddFailure($"Unsupported Platform Type.");
                    }
                    if (value.StatsType != StatsType.Click &&
                        value.StatsType != StatsType.Impression)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.AdStats(value.AdId, value.Platform);
                    var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!keyExists)
                        context.AddFailure($"CacheKey '{redisKey}' doesn't exist.");
                });
        }
    }
}
