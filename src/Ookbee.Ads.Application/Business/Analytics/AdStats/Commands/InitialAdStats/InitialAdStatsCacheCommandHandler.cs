using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStatsById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStatsCache.Commands.InitialAdStats
{
    public class InitialAdStatsCommandHandler : IRequestHandler<InitialAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdStatsCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdStatsCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdList.Ok &&
                    getAdList.Data.HasValue())
                {
                    var items = getAdList.Data;
                    foreach (var item in items)
                    {
                        if (item.Status == AdStatus.Publish || item.Status == AdStatus.Preview)
                            await Mediator.Send(new InitialAdStatsByIdCommand(request.AdUnitId, item.Id, request.CaculatedAt), cancellationToken);
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
