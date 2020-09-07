using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.CreateAdAssetStats;
using Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.GetAdAssetStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsCache;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.InitialAssetAdStats
{
    public class InitialAdAssetStatsCommandHandler : IRequestHandler<InitialAdAssetStatsCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public InitialAdAssetStatsCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(InitialAdAssetStatsCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                var getAdStatsByKey = await Mediator.Send(new GetAdStatsByKeyQuery(request.AdId, platform, request.CaculatedAt), cancellationToken);
                if (!getAdStatsByKey.Ok)
                {
                    var data = getAdStatsByKey.Data;
                    await Mediator.Send(new CreateAdAssetStatsCommand(request.AdId, platform, request.CaculatedAt, 0, 0), cancellationToken);
                }

                var clickStats = getAdStatsByKey?.Data?.Click ?? default(long);
                await Mediator.Send(new CreateAdStatsCacheCommand(request.AdId, platform, request.CaculatedAt, AdStatsType.Click, clickStats), cancellationToken);

                var impressionStats = getAdStatsByKey?.Data?.Impression ?? default(long);
                await Mediator.Send(new CreateAdStatsCacheCommand(request.AdId, platform, request.CaculatedAt, AdStatsType.Impression, impressionStats), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
