using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStats;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.InitialAdUnitStatsCache;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache
{
    public class InitialAdGroupStatsCacheCommandHandler : IRequestHandler<InitialAdGroupStatsCacheCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdGroupStatsCacheCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
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
                        await Mediator.Send(new InitialAdGroupStatsCommand(adGroup.Id, request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new InitialAdUnitStatsCacheCommand(adGroup.Id, request.CaculatedAt), cancellationToken);
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
