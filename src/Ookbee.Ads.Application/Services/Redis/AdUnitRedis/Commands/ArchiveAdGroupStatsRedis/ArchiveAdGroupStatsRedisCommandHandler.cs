using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsList;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.ArchiveAdUnitStatsRedis
{
    public class ArchiveAdUnitRedisCommandHandler : IRequestHandler<ArchiveAdUnitStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo;

        public ArchiveAdUnitRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdUnitStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdUnitStatsList = await Mediator.Send(new GetAdUnitStatsListQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdUnitStatsList.IsSuccess)
                {
                    var adUnitStatsList = getAdUnitStatsList.Data;
                    foreach (var adUnitStats in adUnitStatsList)
                    {
                        var adUnitStatsCache = await Mediator.Send(new GetAdUnitStatsRedisQuery(adUnitStats.AdUnitId), cancellationToken);
                        if (adUnitStatsCache.IsSuccess)
                        {
                            var adUnitStatsDb = await AdUnitStatsDbRepo.FirstAsync(
                                disableTracking: false,
                                filter: f =>
                                    f.AdUnitId == adUnitStats.AdUnitId &&
                                    f.CaculatedAt == request.CaculatedAt
                            );
                            if (adUnitStatsDb.HasValue())
                            {
                                var requestValue = adUnitStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Request).Value;
                                if (requestValue > adUnitStatsDb.Request)
                                    adUnitStatsDb.Request = requestValue;

                                var fillValue = adUnitStatsCache.Data.SingleOrDefault(x => x.Key == AdStatsType.Fill).Value;
                                if (fillValue > adUnitStatsDb.Fill)
                                    adUnitStatsDb.Fill = fillValue;

                                await AdUnitStatsDbRepo.SaveChangesAsync();
                            }
                        }
                    }
                    next = adUnitStatsList.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
