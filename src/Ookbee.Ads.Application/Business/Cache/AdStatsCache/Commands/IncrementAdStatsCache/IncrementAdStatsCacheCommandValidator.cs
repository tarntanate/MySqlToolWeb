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

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != StatsType.Click &&
                        value != StatsType.Impression)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                });
        }
    }
}
