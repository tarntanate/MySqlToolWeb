using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByIdCache
{
    public class ArchiveAdUnitStatsByIdCacheCommandValidator : AbstractValidator<ArchiveAdUnitStatsByIdCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdUnitStatsByIdCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
