using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.ArchiveAdAssetStatsByIdCache
{
    public class ArchiveAdAssetStatsByIdCacheCommandHandler : IRequestHandler<ArchiveAdAssetStatsByIdCacheCommand>
    {
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdStatsEntity> AdAssetStatsDbRepo { get; }

        public ArchiveAdAssetStatsByIdCacheCommandHandler(
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdAssetStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdAssetStatsByIdCacheCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var adGroupStats = await AdAssetStatsDbRepo.FirstAsync(
                    filter: f =>
                        f.AdId == request.AdId &&
                        f.Platform == platform &&
                        f.CaculatedAt == request.CaculatedAt,
                    disableTracking: false
                );
                if (adGroupStats.HasValue())
                {
                    var redisKey = CacheKey.AdStats(request.AdId, platform);
                    var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
                    if (hashEntries.HasValue())
                    {
                        var clickCount = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Click.ToString()).Value;
                        if (clickCount > adGroupStats.Click)
                            adGroupStats.Click = clickCount;

                        var impressionCount = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Impression.ToString()).Value;
                        if (impressionCount > adGroupStats.Impression)
                            adGroupStats.Click = impressionCount;

                        await AdAssetStatsDbRepo.SaveChangesAsync();
                    }
                }
            }
            return Unit.Value;
        }
    }
}
