using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey;
using Ookbee.Ads.Application.Business.Cache.AdStats.Commands.ArchiveAdStats;
using Ookbee.Ads.Application.Business.Cache.AdUnitStats.Commands.ArchiveAdUnitStatsById;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStats.Commands.ArchiveAdUnitStats
{
    public class ArchiveAdUnitStatsCommandHandler : IRequestHandler<ArchiveAdUnitStatsCommand>
    {
        private IMediator Mediator { get; }

        public ArchiveAdUnitStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdUnitStatsList = await Mediator.Send(new GetAdUnitStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdUnitStatsList.Ok)
                {
                    foreach (var adUnit in getAdUnitStatsList.Data)
                    {
                        await Mediator.Send(new ArchiveAdStatsCommand(request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new ArchiveAdUnitStatsByIdCommand(request.CaculatedAt, adUnit.AdUnitId), cancellationToken);
                    }
                    start += length;
                }
                next = getAdUnitStatsList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
