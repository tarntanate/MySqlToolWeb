using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache
{
    public class CreateAdUnitStatsCacheCommandValidator : AbstractValidator<CreateAdUnitStatsCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public CreateAdUnitStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != AdStatsType.Request &&
                        value != AdStatsType.Fill)
                    {
                        context.AddFailure($"Unsupported Stats Type.");
                    }
                });
        }
    }
}
