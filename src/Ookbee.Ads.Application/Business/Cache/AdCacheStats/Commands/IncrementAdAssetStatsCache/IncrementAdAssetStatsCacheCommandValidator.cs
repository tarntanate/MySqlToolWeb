using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetsStatsCache.Commands.IncrementAdAssetsStatsCache
{
    public class IncrementAdAssetStatsCacheCommandValidator : AbstractValidator<IncrementAdAssetsStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdAssetStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.AdStats(value);
                    var redisValue = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!redisValue.HasValue())
                        context.AddFailure($"AdStats '{redisKey}' doesn't exist.");
                });
        }
    }
}
