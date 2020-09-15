using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQueryValidator : AbstractValidator<GetAdUnitCacheByGroupIdQuery>
    {
        private IDatabase AdsRedis { get; }

        public GetAdUnitCacheByGroupIdQueryValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => new { p.AdGroupId, p.Platform })
                .Custom((value, context) =>
                {
                    if (value.Platform == Platform.Unknown)
                    {
                        context.AddFailure($"Unsupported Platform Type.");
                    }
                })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var redisKey = CacheKey.Units(value.AdGroupId);
                    var hashField = value.Platform.ToString();
                    var keyExists = await AdsRedis.HashExistsAsync(redisKey, hashField);
                    if (!keyExists)
                        context.AddFailure($"CacheKey '{redisKey}' doesn't exist.");
                });
        }
    }
}
