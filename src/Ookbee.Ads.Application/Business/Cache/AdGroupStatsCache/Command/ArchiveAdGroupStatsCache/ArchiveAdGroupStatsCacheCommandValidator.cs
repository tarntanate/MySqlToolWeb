using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCache
{
    public class ArchiveAdGroupStatsCacheCommandValidator : AbstractValidator<ArchiveAdGroupStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdGroupStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
