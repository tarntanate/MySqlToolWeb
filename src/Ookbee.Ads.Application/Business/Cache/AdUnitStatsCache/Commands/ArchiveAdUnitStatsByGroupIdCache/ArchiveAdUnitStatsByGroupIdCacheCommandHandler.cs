using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByIdCache;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByGroupIdCache
{
    public class ArchiveAdUnitStatsByGroupIdCacheCommandHandler : IRequestHandler<ArchiveAdUnitStatsByGroupIdCacheCommand>
    {
        private IMediator Mediator { get; }

        public ArchiveAdUnitStatsByGroupIdCacheCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdUnitStatsByGroupIdCacheCommand request, CancellationToken cancellationToken)
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
                        await Mediator.Send(new ArchiveAdUnitStatsByIdCacheCommand(request.CaculatedAt, adUnit.Id), cancellationToken);
                        //await ArchiveAdStats(caculatedAt, adUnit.Id, cancellationToken);
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
