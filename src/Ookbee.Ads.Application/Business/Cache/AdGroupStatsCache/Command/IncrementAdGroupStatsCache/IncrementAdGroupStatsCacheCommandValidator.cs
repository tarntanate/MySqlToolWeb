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

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != StatsType.Request)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                });
        }
    }
}
