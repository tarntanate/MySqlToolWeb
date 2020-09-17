using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Analytics.AdStatsCache.Commands.InitialAdStats;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById;
using Ookbee.Ads.Common.Extensions;
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
                next = false;
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.Ok &&
                    getAdUnitList.Data.HasValue())
                {
                    var items = getAdUnitList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new InitialAdStatsCommand(item.Id, request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new InitialAdUnitStatsByIdCommand(request.AdGroupId, item.Id, request.CaculatedAt), cancellationToken);
                    }
                    start += length;
                    next = items.Count() < length ? false : true;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
