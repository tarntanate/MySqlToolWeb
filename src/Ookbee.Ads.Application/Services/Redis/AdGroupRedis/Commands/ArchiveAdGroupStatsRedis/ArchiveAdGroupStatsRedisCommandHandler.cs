using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsList;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.ArchiveAdGroupStatsRedis
{
    public class ArchiveAdGroupRedisCommandHandler : IRequestHandler<ArchiveAdGroupStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public ArchiveAdGroupRedisCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mediator = mediator;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdGroupStatsList = await Mediator.Send(new GetAdGroupStatsListQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdGroupStatsList.IsSuccess)
                {
                    var adGroupStatsList = getAdGroupStatsList.Data;
                    foreach (var adGroupStats in adGroupStatsList)
                    {
                        var adGroupStatsCache = await Mediator.Send(new GetAdGroupStatsRedisQuery(adGroupStats.AdGroupId), cancellationToken);
                        var adGroupStatsDb = await AdGroupStatsDbRepo.FirstAsync(
                            disableTracking: false,
                            filter: f =>
                                f.AdGroupId == adGroupStats.AdGroupId &&
                                f.CaculatedAt == request.CaculatedAt
                        );
                        if (adGroupStatsDb.HasValue())
                        {
                            var requestValue = adGroupStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Request).Value;
                            if (requestValue > adGroupStatsDb.Request)
                                adGroupStatsDb.Request = requestValue;

                            await AdGroupStatsDbRepo.SaveChangesAsync();
                        }
                    }
                    next = adGroupStatsList.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
