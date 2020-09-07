using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStats
{
    public class InitialAdGroupStatsCommandHandler : IRequestHandler<InitialAdGroupStatsCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public InitialAdGroupStatsCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(InitialAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var getAdGroupStatByKey = await Mediator.Send(new GetAdGroupStatsByKeyQuery(request.AdGroupId, platform, request.CaculatedAt), cancellationToken);
                if (!getAdGroupStatByKey.Ok)
                {
                    var data = getAdGroupStatByKey.Data;
                    await Mediator.Send(new CreateAdGroupStatsCommand(request.AdGroupId, platform, request.CaculatedAt, 0), cancellationToken);
                }
                var requestStats = getAdGroupStatByKey?.Data?.Request ?? default(long);
                await Mediator.Send(new CreateAdGroupStatsCacheCommand(request.AdGroupId, platform, request.CaculatedAt, AdStatsType.Request, requestStats), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
