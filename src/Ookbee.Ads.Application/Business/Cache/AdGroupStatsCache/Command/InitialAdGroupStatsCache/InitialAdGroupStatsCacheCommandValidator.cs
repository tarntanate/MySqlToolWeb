using FluentValidation;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache
{
    public class InitialAdGroupStatsCacheCommandValidator : AbstractValidator<InitialAdGroupStatsCacheCommand>
    {
        public InitialAdGroupStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {

        }
    }
}
