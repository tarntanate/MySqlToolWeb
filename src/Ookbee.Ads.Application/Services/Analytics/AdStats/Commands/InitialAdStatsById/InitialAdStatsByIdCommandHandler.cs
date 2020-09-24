using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdQuotaById;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStatsByKey;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.CreateAdStatsByPlatformCache;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.CreateAdStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.InitialAssetAdStatsById
{
    public class InitialAdStatsByIdCommandHandler : IRequestHandler<InitialAdStatsByIdCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdStatsByIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdStatsByIdCommand request, CancellationToken cancellationToken)
        {
            var getAdQuotaById = await Mediator.Send(new GetAdQuotaByIdQuery(request.AdId, request.CaculatedAt), cancellationToken);
            var quota = getAdQuotaById?.Data ?? default(long);

            if (quota > 0)
            {
                var totalClicks = 0L;
                var totalImpressions = 0L;

                var getAdStatsByKey = await Mediator.Send(new GetAdStatsByKeyQuery(request.AdId, request.CaculatedAt), cancellationToken);
                if (!getAdStatsByKey.Ok)
                {
                    var data = getAdStatsByKey.Data;
                    await Mediator.Send(new CreateAdStatsCommand(request.AdId, request.CaculatedAt, quota, 0, 0), cancellationToken);
                }

                var click = getAdStatsByKey?.Data?.Click ?? default(long);
                await Mediator.Send(new CreateAdStatsByPlatformCacheCommand(request.CaculatedAt, StatsType.Click, request.AdId, click), cancellationToken);
                totalClicks += click;

                var impressions = getAdStatsByKey?.Data?.Impression ?? default(long);
                await Mediator.Send(new CreateAdStatsByPlatformCacheCommand(request.CaculatedAt, StatsType.Impression, request.AdId, impressions), cancellationToken);
                totalImpressions += impressions;

                await Mediator.Send(new CreateAdStatsCacheCommand(request.CaculatedAt, StatsType.Quota, request.AdId, quota), cancellationToken);
                await Mediator.Send(new CreateAdStatsCacheCommand(request.CaculatedAt, StatsType.Click, request.AdId, totalClicks), cancellationToken);
                await Mediator.Send(new CreateAdStatsCacheCommand(request.CaculatedAt, StatsType.Impression, request.AdId, totalImpressions), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
