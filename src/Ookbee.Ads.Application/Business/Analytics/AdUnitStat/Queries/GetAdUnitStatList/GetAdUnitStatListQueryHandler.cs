using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsList.Queries.GetAdUnitStatsList
{
    public class GetAdUnitStatsListQueryHandler : IRequestHandler<GetAdUnitStatsListQuery, HttpResult<IEnumerable<AdUnitStatsDto>>>
    {
        private AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo { get; }

        public GetAdUnitStatsListQueryHandler(AnalyticsDbRepository<AdUnitStatsEntity> adGroupStatsDbRepo)
        {
            AdUnitStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdUnitStatsDto>>> Handle(GetAdUnitStatsListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdUnitStatsEntity>();

            if (request.AdUnitId.HasValue())
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.Platform.HasValue())
                predicate = predicate.And(f => f.Platform == request.Platform);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdUnitStatsDbRepo.FindAsync(
                selector: AdUnitStatsDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.CaculatedAt),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdUnitStatsDto>>();
            return result.Success(items);
        }
    }
}
