using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsListByKey;
using Ookbee.Ads.Application.Business.Cache.AdGroupStats.Commands.ArchiveAdGroupStatsById;
using Ookbee.Ads.Application.Business.Cache.AdUnitStats.Commands.ArchiveAdUnitStats;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStats.Commands.ArchiveAdGroupStats
{
    public class ArchiveAdGroupStatsCommandHandler : IRequestHandler<ArchiveAdGroupStatsCommand>
    {
        private IMediator Mediator { get; }

        public ArchiveAdGroupStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupStatsListByKey = await Mediator.Send(new GetAdGroupStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                if (getAdGroupStatsListByKey.Ok)
                {
                    foreach (var adGroupStats in getAdGroupStatsListByKey.Data)
                    {
                        await Mediator.Send(new ArchiveAdUnitStatsCommand(request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new ArchiveAdGroupStatsByIdCommand(request.CaculatedAt, adGroupStats.AdGroupId), cancellationToken);
                    }
                    start += length;
                }
                next = getAdGroupStatsListByKey.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
