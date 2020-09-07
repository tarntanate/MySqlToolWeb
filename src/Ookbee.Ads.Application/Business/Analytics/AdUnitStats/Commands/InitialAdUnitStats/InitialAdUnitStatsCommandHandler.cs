using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommandHandler : IRequestHandler<InitialAdUnitStatsCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public InitialAdUnitStatsCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var getAdUnitStatsByKey = await Mediator.Send(new GetAdUnitStatsByKeyQuery(request.AdUnitId, platform, request.CaculatedAt), cancellationToken);
                if (!getAdUnitStatsByKey.Ok)
                {
                    var data = getAdUnitStatsByKey.Data;
                    await Mediator.Send(new CreateAdUnitStatsCommand(request.AdUnitId, platform, request.CaculatedAt, 0, 0), cancellationToken);
                }

                var requestStats = getAdUnitStatsByKey?.Data?.Request ?? default(long);
                await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.AdUnitId, platform, request.CaculatedAt, AdStatsType.Request, requestStats), cancellationToken);

                var fillStats = getAdUnitStatsByKey?.Data?.Fill ?? default(long);
                await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.AdUnitId, platform, request.CaculatedAt, AdStatsType.Fill, fillStats), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
