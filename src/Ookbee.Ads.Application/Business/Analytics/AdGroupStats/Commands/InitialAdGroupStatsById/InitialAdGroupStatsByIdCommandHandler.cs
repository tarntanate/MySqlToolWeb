using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache;
using Ookbee.Ads.Infrastructure.Models;
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
            var getAdGroupStatByKey = await Mediator.Send(new GetAdGroupStatsByKeyQuery(request.AdGroupId, request.CaculatedAt), cancellationToken);
            if (!getAdGroupStatByKey.Ok)
            {
                var data = getAdGroupStatByKey.Data;
                await Mediator.Send(new CreateAdGroupStatsCommand(request.CaculatedAt, request.AdGroupId, 0), cancellationToken);
            }
            var requestStats = getAdGroupStatByKey?.Data?.Request ?? default(long);
            await Mediator.Send(new CreateAdGroupStatsCacheCommand(request.CaculatedAt, StatsType.Request, request.AdGroupId, requestStats), cancellationToken);

            return Unit.Value;
        }
    }
}
