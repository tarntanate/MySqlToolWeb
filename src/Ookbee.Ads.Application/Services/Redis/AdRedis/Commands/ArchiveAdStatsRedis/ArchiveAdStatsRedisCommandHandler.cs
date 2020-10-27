using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsByIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsRedis
{
    public class ArchiveAdStatsRedisCommandHandler : IRequestHandler<ArchiveAdStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public ArchiveAdStatsRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(ArchiveAdStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdStatsList = await Mediator.Send(new GetAdStatsListQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdStatsList.IsSuccess)
                {
                    var adStatsList = getAdStatsList.Data;
                    foreach (var adStats in adStatsList)
                    {
                        await Mediator.Send(new ArchiveAdStatsByIdRedisCommand(request.CaculatedAt, adStats.AdId), cancellationToken);
                    }
                    next = adStatsList.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
