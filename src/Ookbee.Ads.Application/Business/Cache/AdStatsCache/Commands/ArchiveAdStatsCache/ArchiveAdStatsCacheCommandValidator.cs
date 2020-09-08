using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsCache
{
    public class ArchiveAdStatsCacheCommandValidator : AbstractValidator<ArchiveAdStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdStatsCacheCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
