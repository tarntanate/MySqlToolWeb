using FluentValidation;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCacheById
{
    public class ArchiveAdGroupStatsCacheByIdCommandValidator : AbstractValidator<ArchiveAdGroupStatsCacheByIdCommand>
    {
        private IDatabase AdsRedis { get; }

        public ArchiveAdGroupStatsCacheByIdCommandValidator(AdsRedisContext adsRedis)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            AdsRedis = adsRedis.Database();
        }
    }
}
