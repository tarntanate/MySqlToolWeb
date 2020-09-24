﻿using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey
{
    public class GetAdUnitStatsListByKeyQueryHandler : IRequestHandler<GetAdUnitStatsListByKeyQuery, Response<IEnumerable<AdUnitStatsDto>>>
    {
        private readonly AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo;

        public GetAdUnitStatsListByKeyQueryHandler(AnalyticsDbRepository<AdUnitStatsEntity> adGroupStatsDbRepo)
        {
            AdUnitStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<IEnumerable<AdUnitStatsDto>>> Handle(GetAdUnitStatsListByKeyQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdUnitStatsEntity>();

            if (request.AdUnitId.HasValue())
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdUnitStatsDbRepo.FindAsync(
                selector: AdUnitStatsDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.CaculatedAt),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdUnitStatsDto>>();
            return result.Success(items);
        }
    }
}
