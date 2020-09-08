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

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsCache
{
    public class ArchiveAdStatsCacheCommandHandler : IRequestHandler<ArchiveAdStatsCacheCommand>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public ArchiveAdStatsCacheCommandHandler(
            IMapper mapper,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database();
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdStatsCacheCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var adGroupStats = await AdStatsDbRepo.FirstAsync(
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
                        adGroupStats.Click = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Click.ToString()).Value;
                        adGroupStats.Impression = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Impression.ToString()).Value;
                        await AdStatsDbRepo.SaveChangesAsync();
                    }
                }
            }
            return Unit.Value;
        }
    }
}
