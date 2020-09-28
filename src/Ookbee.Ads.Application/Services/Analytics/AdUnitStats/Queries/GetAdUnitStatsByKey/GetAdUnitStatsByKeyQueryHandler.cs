﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey
{
    public class GetAdUnitStatsByKeyQueryHandler : IRequestHandler<GetAdUnitStatsByKeyQuery, Response<AdUnitStatsDto>>
    {
        private readonly AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo;

        public GetAdUnitStatsByKeyQueryHandler(AnalyticsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Response<AdUnitStatsDto>> Handle(GetAdUnitStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitStatsDbRepo.FirstAsync(
                selector: AdUnitStatsDto.Projection,
                filter: f =>
                    f.AdUnitId == request.AdUnitId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<AdUnitStatsDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
