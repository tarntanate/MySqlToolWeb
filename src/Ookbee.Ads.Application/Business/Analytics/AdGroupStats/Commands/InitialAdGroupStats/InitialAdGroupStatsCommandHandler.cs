using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStatsById;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStatsCache.Commands.InitialAdUnitStats;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsCache.Commands.InitialAdGroupStats
{
    public class InitialAdGroupStatsCommandHandler : IRequestHandler<InitialAdGroupStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdGroupStatsCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok &&
                    getAdGroupList.Data.HasValue())
                {
                    var items = getAdGroupList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new InitialAdUnitStatsCommand(request.CaculatedAt, item.Id), cancellationToken);
                        await Mediator.Send(new InitialAdGroupStatsByIdCommand(request.CaculatedAt, item.Id), cancellationToken);
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
