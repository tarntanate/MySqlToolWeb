using MediatR;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsByIdRedis
{
    public class ArchiveAdStatsByIdRedisCommandHandler : IRequestHandler<ArchiveAdStatsByIdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public ArchiveAdStatsByIdRedisCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mediator = mediator;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdStatsByIdRedisCommand request, CancellationToken cancellationToken)
        {
            var adStatsDb = await AdStatsDbRepo.FirstAsync(
                disableTracking: false,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.CaculatedAt == request.CaculatedAt
            );
            if (adStatsDb.HasValue())
            {
                var adStatsCache = await Mediator.Send(new GetAdStatsRedisQuery(request.AdId), cancellationToken);
                if (adStatsCache.IsSuccess)
                {
                    var quotaStats = adStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Quota).Value;

                    var clickStats = adStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Click).Value;
                    if (clickStats > adStatsDb.Click)
                        adStatsDb.Click = clickStats;

                    var impressionStats = adStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Impression).Value;
                    if (impressionStats > adStatsDb.Impression)
                        adStatsDb.Impression = (impressionStats > quotaStats) ? quotaStats : impressionStats;

                    await AdStatsDbRepo.SaveChangesAsync();
                }
            }

            return Unit.Value;
        }
    }
}
