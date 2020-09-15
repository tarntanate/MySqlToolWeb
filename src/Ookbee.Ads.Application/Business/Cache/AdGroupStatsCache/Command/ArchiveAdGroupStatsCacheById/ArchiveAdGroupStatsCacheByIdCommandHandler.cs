using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCacheById
{
    public class ArchiveAdGroupStatsCacheByIdCommandHandler : IRequestHandler<ArchiveAdGroupStatsCacheByIdCommand>
    {
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public ArchiveAdGroupStatsCacheByIdCommandHandler(
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsCacheByIdCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in EnumHelper.GetValues<Platform>())
            {
                if (platform != Platform.Unknown)
                {
                    var adGroupStats = await AdGroupStatsDbRepo.FirstAsync(
                        filter: f =>
                            f.AdGroupId == request.AdGroupId &&
                            f.Platform == platform &&
                            f.CaculatedAt == request.CaculatedAt,
                        disableTracking: false
                    );
                    if (adGroupStats.HasValue())
                    {
                        var redisKey = CacheKey.GroupStats(request.AdGroupId, platform);
                        var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
                        if (hashEntries.HasValue())
                        {
                            var requestCount = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == StatsType.Request.ToString()).Value;
                            if (requestCount > adGroupStats.Request)
                                adGroupStats.Request = requestCount;

                            await AdGroupStatsDbRepo.SaveChangesAsync();
                        }
                    }
                }
            }
            return Unit.Value;
        }
    }
}
