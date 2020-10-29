using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQueryHandler : IRequestHandler<GetAdGroupStatsListQuery, Response<IEnumerable<AdGroupStatsDto>>>
    {
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public GetAdGroupStatsListQueryHandler(
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<IEnumerable<AdGroupStatsDto>>> Handle(GetAdGroupStatsListQuery request, CancellationToken cancellationToken)
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
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
