using AutoMapper;
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

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCache
{
    public class ArchiveAdGroupStatsCacheCommandHandler : IRequestHandler<ArchiveAdGroupStatsCacheCommand>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public ArchiveAdGroupStatsCacheCommandHandler(
            IMapper mapper,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database();
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
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
                        adGroupStats.Request = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Request.ToString()).Value;
                        await AdGroupStatsDbRepo.SaveChangesAsync();
                    }
                }
            }
            return Unit.Value;
        }
    }
}
