using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsCache
{
    public class ArchiveAdUnitStatsCacheCommandValidator : AbstractValidator<ArchiveAdUnitStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdUnitStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
