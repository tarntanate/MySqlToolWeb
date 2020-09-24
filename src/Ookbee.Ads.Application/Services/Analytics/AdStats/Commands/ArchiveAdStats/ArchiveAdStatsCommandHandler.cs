using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsListByKey;
using Ookbee.Ads.Application.Services.Cache.AdStats.Commands.ArchiveAdStatsById;
using Ookbee.Ads.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdStats.Commands.ArchiveAdStats
{
    public class ArchiveAdStatsCommandHandler : IRequestHandler<ArchiveAdStatsCommand>
    {
        private readonly IMediator Mediator;

        public ArchiveAdStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdStatsList = await Mediator.Send(new GetAdStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdStatsList.Ok &&
                    getAdStatsList.Data.HasValue())
                {
                    var items = getAdStatsList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new ArchiveAdStatsByIdCommand(request.CaculatedAt, item.AdId), cancellationToken);
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
