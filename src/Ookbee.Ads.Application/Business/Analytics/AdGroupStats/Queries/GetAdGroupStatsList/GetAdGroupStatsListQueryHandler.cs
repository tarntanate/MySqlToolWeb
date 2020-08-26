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

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQueryHandler : IRequestHandler<GetAdGroupStatsListQuery, HttpResult<IEnumerable<AdGroupStatsDto>>>
    {
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public GetAdGroupStatsListQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdGroupStatsDto>>> Handle(GetAdGroupStatsListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdGroupStatsEntity>();

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            if (request.Platform.HasValue())
                predicate = predicate.And(f => f.Platform == request.Platform);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdGroupStatsDbRepo.FindAsync(
                selector: AdGroupStatsDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.CaculatedAt),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdGroupStatsDto>>();
            return result.Success(items);
        }
    }
}
