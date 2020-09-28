using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey;
using Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById
{
    public class InitialAdUnitStatsByIdCommandHandler : IRequestHandler<InitialAdUnitStatsByIdCommand>
    {
        private readonly IMediator Mediator;

        public InitialAdUnitStatsByIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsByIdCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitStatsByKey = await Mediator.Send(new GetAdUnitStatsByKeyQuery(request.AdUnitId, request.CaculatedAt), cancellationToken);
            if (!getAdUnitStatsByKey.IsSuccess)
            {
                var data = getAdUnitStatsByKey.Data;
                await Mediator.Send(new CreateAdUnitStatsCommand(request.CaculatedAt, request.AdUnitId, 0, 0), cancellationToken);
            }

            var requestStats = getAdUnitStatsByKey?.Data?.Request ?? default(long);
            await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.CaculatedAt, request.AdUnitId, AdStatsType.Request, requestStats), cancellationToken);

            var fillStats = getAdUnitStatsByKey?.Data?.Fill ?? default(long);
            await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.CaculatedAt, request.AdUnitId, AdStatsType.Fill, fillStats), cancellationToken);

            return Unit.Value;
        }
    }
}
