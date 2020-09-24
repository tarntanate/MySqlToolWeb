using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey;
using Ookbee.Ads.Application.Services.Cache.AdStats.Commands.ArchiveAdStats;
using Ookbee.Ads.Application.Services.Cache.AdUnitStats.Commands.ArchiveAdUnitStatsById;
using Ookbee.Ads.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStats.Commands.ArchiveAdUnitStats
{
    public class ArchiveAdUnitStatsCommandHandler : IRequestHandler<ArchiveAdUnitStatsCommand>
    {
        private readonly IMediator Mediator;

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
                next = false;
                var getAdUnitStatsList = await Mediator.Send(new GetAdUnitStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdUnitStatsList.Ok &&
                    getAdUnitStatsList.Data.HasValue())
                {
                    var items = getAdUnitStatsList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new ArchiveAdStatsCommand(request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new ArchiveAdUnitStatsByIdCommand(request.CaculatedAt, item.AdUnitId), cancellationToken);
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
