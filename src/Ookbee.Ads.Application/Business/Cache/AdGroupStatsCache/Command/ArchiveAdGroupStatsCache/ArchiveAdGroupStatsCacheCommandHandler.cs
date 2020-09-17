using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCacheById;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsByGroupIdCache;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCache
{
    public class ArchiveAdGroupStatsCacheCommandHandler : IRequestHandler<ArchiveAdGroupStatsCacheCommand>
    {
        private IMediator Mediator { get; }

        public ArchiveAdGroupStatsCacheCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok)
                {
                    foreach (var adGroup in getAdGroupList.Data)
                    {
                        await Mediator.Send(new ArchiveAdGroupStatsCacheByIdCommand(request.CaculatedAt, adGroup.Id), cancellationToken);
                        await Mediator.Send(new ArchiveAdUnitStatsByGroupIdCacheCommand(request.CaculatedAt, adGroup.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdGroupList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
