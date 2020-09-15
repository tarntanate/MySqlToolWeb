using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsByIdCache
{
    public class ArchiveAdStatsByIdCacheCommandHandler : IRequestHandler<ArchiveAdStatsByIdCacheCommand>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public ArchiveAdStatsByIdCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdStatsByIdCacheCommand request, CancellationToken cancellationToken)
        {
            var adStats = await AdStatsDbRepo.FirstAsync(
                filter: f =>
                    f.AdId == request.AdId &&
                    f.CaculatedAt == request.CaculatedAt,
                disableTracking: false
            );
            if (adStats.HasValue())
            {
                var redisKey = CacheKey.AdStats(request.AdId);
                var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
                if (hashEntries.HasValue())
                {
                    var clickCount = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == StatsType.Click.ToString()).Value;
                    if (clickCount > adStats.Click)
                        adStats.Click = clickCount;

                    var impressionCount = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == StatsType.Impression.ToString()).Value;
                    if (impressionCount > adStats.Impression)
                        adStats.Impression = impressionCount;

                    await AdStatsDbRepo.SaveChangesAsync();
                }
            }

            return Unit.Value;
        }
    }
}
