﻿using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdQuotaById;
using Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsByPlatformCache;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsCache;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStatsById
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
            var quotaStats = getAdQuotaById?.Data ?? default(long);
            await Mediator.Send(new CreateAdStatsCacheCommand(request.CaculatedAt, StatsType.Quota, request.AdId, quotaStats), cancellationToken);

            foreach (var platform in EnumHelper.GetValues<Platform>())
            {
                if (platform != Platform.Unknown)
                {
                    var getAdStatsByKey = await Mediator.Send(new GetAdStatsByKeyQuery(request.AdId, platform, request.CaculatedAt), cancellationToken);
                    if (!getAdStatsByKey.Ok)
                    {
                        var data = getAdStatsByKey.Data;
                        await Mediator.Send(new CreateAdStatsCommand(request.AdId, platform, request.CaculatedAt, getAdQuotaById.Data, 0, 0), cancellationToken);
                    }

                    var clickStats = getAdStatsByKey?.Data?.Click ?? default(long);
                    await Mediator.Send(new CreateAdStatsByPlatformCacheCommand(request.CaculatedAt, platform, StatsType.Click, request.AdId, clickStats), cancellationToken);

                    var impressionStats = getAdStatsByKey?.Data?.Impression ?? default(long);
                    await Mediator.Send(new CreateAdStatsByPlatformCacheCommand(request.CaculatedAt, platform, StatsType.Impression, request.AdId, impressionStats), cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
