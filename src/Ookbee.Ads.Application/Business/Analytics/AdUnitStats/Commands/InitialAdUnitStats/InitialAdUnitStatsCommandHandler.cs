﻿using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStats
{
    public class InitialAdUnitStatsCommandHandler : IRequestHandler<InitialAdUnitStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdUnitStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            foreach (var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>())
            {
                if (platform != Platform.Unknown)
                {
                    var getAdUnitStatsByKey = await Mediator.Send(new GetAdUnitStatsByKeyQuery(request.AdUnitId, platform, request.CaculatedAt), cancellationToken);
                    if (!getAdUnitStatsByKey.Ok)
                    {
                        var data = getAdUnitStatsByKey.Data;
                        await Mediator.Send(new CreateAdUnitStatsCommand(request.AdUnitId, platform, request.CaculatedAt, 0, 0), cancellationToken);
                    }

                    var requestStats = getAdUnitStatsByKey?.Data?.Request ?? default(long);
                    await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.AdUnitId, platform, request.CaculatedAt, StatsType.Request, requestStats), cancellationToken);

                    var fillStats = getAdUnitStatsByKey?.Data?.Fill ?? default(long);
                    await Mediator.Send(new CreateAdUnitStatsCacheCommand(request.AdUnitId, platform, request.CaculatedAt, StatsType.Fill, fillStats), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
