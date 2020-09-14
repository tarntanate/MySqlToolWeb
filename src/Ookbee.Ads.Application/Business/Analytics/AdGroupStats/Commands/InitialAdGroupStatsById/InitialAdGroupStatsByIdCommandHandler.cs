using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStatsById
{
    public class InitialAdGroupStatsByIdCommandHandler : IRequestHandler<InitialAdGroupStatsByIdCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdGroupStatsByIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdGroupStatsByIdCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                if (platform != Platform.Unknown)
                {
                    var getAdGroupStatByKey = await Mediator.Send(new GetAdGroupStatsByKeyQuery(request.AdGroupId, platform, request.CaculatedAt), cancellationToken);
                    if (!getAdGroupStatByKey.Ok)
                    {
                        var data = getAdGroupStatByKey.Data;
                        await Mediator.Send(new CreateAdGroupStatsCommand(request.CaculatedAt, platform, request.AdGroupId, 0), cancellationToken);
                    }
                    var requestStats = getAdGroupStatByKey?.Data?.Request ?? default(long);
                    await Mediator.Send(new CreateAdGroupStatsCacheCommand(request.CaculatedAt, platform, StatsType.Request, request.AdGroupId, requestStats), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
