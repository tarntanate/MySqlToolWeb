﻿using AutoMapper;
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

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsCache
{
    public class ArchiveAdUnitStatsCacheCommandHandler : IRequestHandler<ArchiveAdUnitStatsCacheCommand>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo { get; }

        public ArchiveAdUnitStatsCacheCommandHandler(
            IMapper mapper,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database();
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var adGroupStats = await AdUnitStatsDbRepo.FirstAsync(
                    filter: f =>
                        f.AdUnitId == request.AdUnitId &&
                        f.Platform == platform &&
                        f.CaculatedAt == request.CaculatedAt,
                    disableTracking: false
                );
                if (adGroupStats.HasValue())
                {
                    var redisKey = CacheKey.UnitsStats(request.AdUnitId, platform);
                    var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
                    if (hashEntries.HasValue())
                    {
                        adGroupStats.Request = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Request.ToString()).Value;
                        adGroupStats.Fill = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == AdStatsType.Fill.ToString()).Value;
                        await AdUnitStatsDbRepo.SaveChangesAsync();
                    }
                }
            }
            return Unit.Value;
        }
    }
}
