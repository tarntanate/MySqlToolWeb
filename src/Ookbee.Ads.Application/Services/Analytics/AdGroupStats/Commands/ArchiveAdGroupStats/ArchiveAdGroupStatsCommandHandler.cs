using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsListByKey;
using Ookbee.Ads.Application.Services.Cache.AdGroupStats.Commands.ArchiveAdGroupStatsById;
using Ookbee.Ads.Application.Services.Cache.AdUnitStats.Commands.ArchiveAdUnitStats;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStats.Commands.ArchiveAdGroupStats
{
    public class ArchiveAdGroupStatsCommandHandler : IRequestHandler<ArchiveAdGroupStatsCommand>
    {
        private readonly IMediator Mediator;

        public ArchiveAdGroupStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var start = 0;
                var length = 100;
                var next = true;
                do
                {
                    next = false;
                    var getAdGroupStatsListByKey = await Mediator.Send(new GetAdGroupStatsListByKeyQuery(start, length, null, request.CaculatedAt), cancellationToken);
                    if (getAdGroupStatsListByKey.IsSuccess &&
                        getAdGroupStatsListByKey.Data.HasValue())
                    {
                        var items = getAdGroupStatsListByKey.Data;
                        foreach (var item in items)
                        {
                            await Mediator.Send(new ArchiveAdUnitStatsCommand(request.CaculatedAt), cancellationToken);
                            await Mediator.Send(new ArchiveAdGroupStatsByIdCommand(request.CaculatedAt, item.AdGroupId), cancellationToken);
                        }
                        start += length;
                        next = items.Count() < length ? false : true;
                    }
                }
                while (next);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
