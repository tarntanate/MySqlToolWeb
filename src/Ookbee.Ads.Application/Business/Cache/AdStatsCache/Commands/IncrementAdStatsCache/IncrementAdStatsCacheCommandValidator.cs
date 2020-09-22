using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommandValidator : AbstractValidator<IncrementAdStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdId, p.StatsType })
                .Custom((value, context) =>
                {
                    if (value.StatsType != StatsType.Click &&
                        value.StatsType != StatsType.Impression)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.AdStats(value.AdId);
                    var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!keyExists)
                        context.AddFailure($"Unable to update stats: Invalid or expired key.");
                });
        }
    }
}
