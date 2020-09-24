using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.DeleteAdStatsCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common;
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

namespace Ookbee.Ads.Application.Services.Cache.AdStats.Commands.ArchiveAdStatsById
{
    public class ArchiveAdStatsByIdCommandHandler : IRequestHandler<ArchiveAdStatsByIdCommand>
    {
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }
        private readonly AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public ArchiveAdStatsByIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdStatsByIdCommand request, CancellationToken cancellationToken)
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
                    var clicks = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == StatsType.Click.ToString()).Value;
                    if (clicks > adStats.Click) adStats.Click = clicks;
                    if (adStats.Click > adStats.Quota) adStats.Click = adStats.Quota;

                    var impressions = (long)hashEntries.FirstOrDefault(hashEntry => hashEntry.Name == StatsType.Impression.ToString()).Value;
                    if (impressions > adStats.Impression) adStats.Impression = impressions;
                    if (adStats.Impression > adStats.Quota) adStats.Impression = adStats.Quota;

                    await AdStatsDbRepo.SaveChangesAsync();
                }

                if (adStats.Impression >= adStats.Quota)
                {
                    var caculatedAt = MechineDateTime.Date;
                    await Mediator.Send(new DeleteAdCacheCommand(request.AdId), cancellationToken);
                    await Mediator.Send(new DeleteAdStatsCacheCommand(request.AdId), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
