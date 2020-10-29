using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsIdList
{
    public class GetAdGroupStatsIdListQueryHandler : IRequestHandler<GetAdGroupStatsIdListQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public GetAdGroupStatsIdListQueryHandler(
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdGroupStatsIdListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdGroupStatsEntity>();

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdGroupStatsDbRepo.FindAsync(
                selector: f => new { Id = f.Id },
                filter: predicate,
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<long>>();
            return (items.HasValue())
                ? result.OK(items.Select(x => x.Id).ToList())
                : result.NotFound();
        }
    }
}
