using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache
{
    public class IncrementAdGroupStatsCacheCommandValidator : AbstractValidator<IncrementAdGroupStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdGroupStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdGroupId, p.StatsType })
                .Custom((value, context) =>
                {
                    if (value.StatsType != StatsType.Request)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.GroupStats(value.AdGroupId);
                    var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!keyExists)
                        context.AddFailure($"Unable to update stats: Invalid or expired key.");
                });
        }
    }
}
