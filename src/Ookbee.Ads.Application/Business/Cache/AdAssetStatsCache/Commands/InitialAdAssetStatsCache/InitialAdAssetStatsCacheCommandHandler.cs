using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.InitialAssetAdStats;
using Ookbee.Ads.Infrastructure.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.InitialAdAssetStatsCache
{
    public class InitialAdAssetStatsCacheCommandHandler : IRequestHandler<InitialAdAssetStatsCacheCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdAssetStatsCacheCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdAssetStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdList.Ok)
                {
                    foreach (var ad in getAdList.Data)
                    {
                        if (ad.Status == AdStatus.Publish || ad.Status == AdStatus.Preview)
                            await Mediator.Send(new InitialAdAssetStatsCommand(ad.Id, request.CaculatedAt), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }

    }
}
