using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStatsById
{
    public class InitialAdStatsByIdCommandHandler : IRequestHandler<InitialAdStatsByIdCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdStatsByIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdStatsByIdCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                if (platform != Platform.Unknown)
                {
                    var getAdStatsByKey = await Mediator.Send(new GetAdStatsByKeyQuery(request.AdId, platform, request.CaculatedAt), cancellationToken);
                    if (!getAdStatsByKey.Ok)
                    {
                        var data = getAdStatsByKey.Data;
                        await Mediator.Send(new CreateAdStatsCommand(request.AdId, platform, request.CaculatedAt, 0, 0), cancellationToken);
                    }

                    var clickStats = getAdStatsByKey?.Data?.Click ?? default(long);
                    await Mediator.Send(new CreateAdStatsCacheCommand(request.AdId, platform, request.CaculatedAt, StatsType.Click, clickStats), cancellationToken);

                    var impressionStats = getAdStatsByKey?.Data?.Impression ?? default(long);
                    await Mediator.Send(new CreateAdStatsCacheCommand(request.AdId, platform, request.CaculatedAt, StatsType.Impression, impressionStats), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
