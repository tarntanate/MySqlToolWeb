﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQueryHandler : IRequestHandler<IsExistsAdGroupStatsByKeyQuery, Response<bool>>
    {
        private readonly AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public IsExistsAdGroupStatsByKeyQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdGroupStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupStatsDbRepo.AnyAsync(f =>
                f.AdGroupId == request.AdGroupId &&
                f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
