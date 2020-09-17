﻿using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStatsById
{
    public class InitialAdUnitStatsByIdCommandHandler : IRequestHandler<InitialAdUnitStatsByIdCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdUnitStatsByIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsByIdCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitStatsByKey = await Mediator.Send(new GetAdUnitStatsByKeyQuery(request.AdUnitId, request.CaculatedAt), cancellationToken);
            if (!getAdUnitStatsByKey.Ok)
            {
                var data = getAdUnitStatsByKey.Data;
                await Mediator.Send(new CreateAdUnitStatsCommand(request.CaculatedAt, request.AdUnitId, 0, 0), cancellationToken);
            }

            var requestStats = getAdUnitStatsByKey?.Data?.Request ?? default(long);
            await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.CaculatedAt, request.AdUnitId, StatsType.Request, requestStats), cancellationToken);

            var fillStats = getAdUnitStatsByKey?.Data?.Fill ?? default(long);
            await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.CaculatedAt, request.AdUnitId, StatsType.Fill, fillStats), cancellationToken);

            return Unit.Value;
        }
    }
}