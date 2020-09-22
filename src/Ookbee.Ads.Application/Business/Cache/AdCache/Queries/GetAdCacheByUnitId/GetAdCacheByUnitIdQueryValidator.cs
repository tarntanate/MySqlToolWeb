using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdCacheByUnitIdQueryValidator : AbstractValidator<GetAdByUnitIdQuery>
    {
        private IDatabase AdsRedis { get; }

        public GetAdCacheByUnitIdQueryValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdUnitId, p.Platform })
                .Custom((value, context) =>
                {
                    if (value.Platform == Platform.Unknown)
                    {
                        context.AddFailure($"Unsupported Platform Type.");
                    }
                })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.Units(value.AdUnitId);
                    var hashField = value.Platform.ToString();
                    var keyExists = await AdsRedis.HashExistsAsync(redisKey, hashField);
                    if (!keyExists)
                        context.AddFailure($"Data not found.");
                });
        }
    }
}
