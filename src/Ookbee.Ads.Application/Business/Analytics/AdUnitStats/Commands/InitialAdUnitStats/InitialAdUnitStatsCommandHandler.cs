using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Analytics.AdStatsCache.Commands.InitialAdStats;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsCache.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommandHandler : IRequestHandler<InitialAdUnitStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdUnitStatsCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.Ok)
                {
                    foreach (var adUnit in getAdUnitList.Data)
                    {
                        await Mediator.Send(new InitialAdStatsCommand(adUnit.Id, request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new InitialAdUnitStatsByIdCommand(request.AdGroupId, adUnit.Id, request.CaculatedAt), cancellationToken);
                    }
                    start += length;
                }
                next = getAdUnitList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
