using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.InitialAssetAdStatsById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStatsCache.Commands.InitialAdStats
{
    public class InitialAdStatsCommandHandler : IRequestHandler<InitialAdStatsCommand>
    {
        private readonly IMediator Mediator;

        public InitialAdStatsCommandHandler(
            IMediator mediator)
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
                var getAdListRequest = new GetAdListQuery(start, length, request.AdUnitId, null);
                var getAdListResponse = await Mediator.Send(getAdListRequest, cancellationToken);
                if (getAdListResponse.Data.HasValue())
                {
                    var items = getAdListResponse.Data;
                    foreach (var item in items)
                    {
                        if (item.Status == AdStatusType.Publish || item.Status == AdStatusType.Preview)
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
