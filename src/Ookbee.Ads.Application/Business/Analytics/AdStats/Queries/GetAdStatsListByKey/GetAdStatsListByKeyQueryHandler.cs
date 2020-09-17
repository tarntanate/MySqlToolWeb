using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdStats;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStat.Queries.GetAdStatsListByKey
{
    public class GetAdStatsListByKeyQueryHandler : IRequestHandler<GetAdStatsListByKeyQuery, HttpResult<IEnumerable<AdStatsDto>>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdStatsListByKeyQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdStatsDto>>> Handle(GetAdStatsListByKeyQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdStatsEntity>();

            if (request.AdId.HasValue())
                predicate = predicate.And(f => f.AdId == request.AdId);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdStatsDbRepo.FindAsync(
                selector: AdStatsDto.Projection,
                filter: predicate,
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdStatsDto>>();
            return (items.HasValue())
                ? result.Success(items)
                : result.Fail(404, $"Data not found.");
        }
    }
}
