using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.GetAdUnitStatsCache;
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

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStats.Commands.ArchiveAdUnitStatsById
{
    public class ArchiveAdUnitStatsByIdCommandHandler : IRequestHandler<ArchiveAdUnitStatsByIdCommand>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }
        private AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo { get; }

        public ArchiveAdUnitStatsByIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdUnitStatsByIdCommand request, CancellationToken cancellationToken)
        {
            var adUnitStats = await AdUnitStatsDbRepo.FirstAsync(
                filter: f =>
                    f.AdUnitId == request.AdUnitId &&
                    f.CaculatedAt == request.CaculatedAt,
                disableTracking: false
            );

            if (adUnitStats.HasValue())
            {
                var getAdUnitStatsCache = await Mediator.Send(new GetAdUnitStatsCacheQuery(request.AdUnitId), cancellationToken);
                if (getAdUnitStatsCache.Ok &&
                    getAdUnitStatsCache.Data.HasValue())
                {
                    var data = getAdUnitStatsCache.Data;

                    var requests = data.SingleOrDefault(x => x.Key == StatsType.Request.ToString()).Value;
                    if (requests > adUnitStats.Request)
                        adUnitStats.Request = requests;

                    var fills = data.SingleOrDefault(x => x.Key == StatsType.Fill.ToString()).Value;
                    if (fills > adUnitStats.Fill)
                        adUnitStats.Fill = fills;

                    await AdUnitStatsDbRepo.SaveChangesAsync();
                }
            }

            return Unit.Value;
        }
    }
}
