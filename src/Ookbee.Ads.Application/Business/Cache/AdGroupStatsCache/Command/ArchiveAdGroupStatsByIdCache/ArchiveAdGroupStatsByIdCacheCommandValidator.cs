using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsByIdCache
{
    public class ArchiveAdGroupStatsByIdCacheCommandValidator : AbstractValidator<ArchiveAdGroupStatsByIdCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdGroupStatsByIdCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
