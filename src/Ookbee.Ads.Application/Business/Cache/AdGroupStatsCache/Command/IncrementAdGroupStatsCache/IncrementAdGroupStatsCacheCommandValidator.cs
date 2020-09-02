using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
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

            RuleFor(p => new { p.AdGroupId, p.Platform })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.UnitsAdIds(value.AdGroupId, value.Platform);
                    var redisValue = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!redisValue.HasValue())
                        context.AddFailure($"GroupStats '{redisKey}' doesn't exist.");
                });
        }
    }
}
