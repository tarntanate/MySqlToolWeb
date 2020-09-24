﻿using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.GetAdGroupStatsCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStats.Commands.ArchiveAdGroupStatsById
{
    public class ArchiveAdGroupStatsByIdCommandHandler : IRequestHandler<ArchiveAdGroupStatsByIdCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public ArchiveAdGroupStatsByIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsByIdCommand request, CancellationToken cancellationToken)
        {
            var adGroupStats = await AdGroupStatsDbRepo.FirstAsync(
                disableTracking: false,
                filter: f =>
                    f.AdGroupId == request.AdGroupId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            if (adGroupStats.HasValue())
            {
                var getAdGroupStatsCache = await Mediator.Send(new GetAdGroupStatsCacheQuery(request.AdGroupId), cancellationToken);
                if (getAdGroupStatsCache.IsSuccess &&
                    getAdGroupStatsCache.Data.HasValue())
                {
                    var data = getAdGroupStatsCache.Data;

                    var requests = data.SingleOrDefault(x => x.Key == AdStatsType.Request.ToString()).Value;
                    if (requests > adGroupStats.Request)
                        adGroupStats.Request = requests;

                    await AdGroupStatsDbRepo.SaveChangesAsync();
                }
            }

            return Unit.Value;
        }
    }
}
