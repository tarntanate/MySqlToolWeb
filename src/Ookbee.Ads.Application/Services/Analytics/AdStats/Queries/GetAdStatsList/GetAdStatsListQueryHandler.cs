﻿using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStats;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList
{
    public class GetAdStatsListQueryHandler : IRequestHandler<GetAdStatsListQuery, Response<IEnumerable<AdStatsDto>>>
    {
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public GetAdStatsListQueryHandler(AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<IEnumerable<AdStatsDto>>> Handle(GetAdStatsListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdStatsEntity>();

            if (request.AdId.HasValue())
                predicate = predicate.And(f => f.AdId == request.AdId);

            if (request.AdUnitId.HasValue())
                predicate = predicate.And(f => f.Ad.AdUnitId == request.AdUnitId);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdStatsDbRepo.FindAsync(
                selector: AdStatsDto.Projection,
                filter: predicate,
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdStatsDto>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
