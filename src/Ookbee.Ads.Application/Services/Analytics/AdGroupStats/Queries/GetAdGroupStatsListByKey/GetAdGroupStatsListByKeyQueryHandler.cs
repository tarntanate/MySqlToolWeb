using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsListByKey
{
    public class GetAdGroupStatsListByKeyQueryHandler : IRequestHandler<GetAdGroupStatsListByKeyQuery, Response<IEnumerable<AdGroupStatsDto>>>
    {
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public GetAdGroupStatsListByKeyQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<IEnumerable<AdGroupStatsDto>>> Handle(GetAdGroupStatsListByKeyQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdGroupStatsEntity>();

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdGroupStatsDbRepo.FindAsync(
                selector: AdGroupStatsDto.Projection,
                filter: predicate,
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdGroupStatsDto>>();

            var x = items.HasValue();

            return (items.HasValue())
                ? result.Success(items)
                : result.Fail(404, $"Data not found.");
        }
    }
}
