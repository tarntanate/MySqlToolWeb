using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.IncrementAdAssetStatsCache
{
    public class IncrementAdAssetStatsCacheCommandValidator : AbstractValidator<IncrementAdAssetStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public IncrementAdAssetStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdId, p.Platform })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.AdStats(value.AdId, value.Platform);
                    var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                    if (!keyExists)
                        context.AddFailure($"AdAssetStats '{redisKey}' doesn't exist.");
                });
        }
    }
}
