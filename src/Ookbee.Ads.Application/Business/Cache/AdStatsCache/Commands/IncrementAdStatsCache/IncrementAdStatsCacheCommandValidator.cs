using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
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

            RuleFor(p => new { p.AdId, p.Platform })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.AdStats(value.AdId, value.Platform);
                    var redisValue = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!redisValue.HasValue())
                        context.AddFailure($"AdStats '{redisKey}' doesn't exist.");
                });
        }
    }
}
