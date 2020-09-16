using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdStat.Queries.GetAdStatsListByKey;
using Ookbee.Ads.Application.Business.Cache.AdStats.Commands.ArchiveAdStatsById;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdStats.Commands.ArchiveAdStats
{
    public class ArchiveAdStatsCommandHandler : IRequestHandler<ArchiveAdStatsCommand>
    {
        private IMediator Mediator { get; }

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
                var getAdStatsList = await Mediator.Send(new GetAdStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdStatsList.Ok)
                {
                    foreach (var ad in getAdStatsList.Data)
                    {
                        await Mediator.Send(new ArchiveAdStatsByIdCommand(request.CaculatedAt, ad.AdId), cancellationToken);
                    }
                    start += length;
                }
                next = getAdStatsList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
