using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStats;
using Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.InitialAdAssetStatsCache;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.InitialAdUnitStatsCache
{
    public class InitialAdUnitStatsCacheCommandHandler : IRequestHandler<InitialAdUnitStatsCacheCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdUnitStatsCacheCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsCacheCommand request, CancellationToken cancellationToken)
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
                        await Mediator.Send(new InitialAdUnitStatsCommand(adUnit.Id, request.CaculatedAt), cancellationToken);
                        await Mediator.Send(new InitialAdAssetStatsCacheCommand(adUnit.Id, request.CaculatedAt), cancellationToken);
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
