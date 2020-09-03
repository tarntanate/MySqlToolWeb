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

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStats.Queries.GetAdStatsList
{
    public class GetAdStatsListQueryHandler : IRequestHandler<GetAdStatsListQuery, HttpResult<IEnumerable<AdStatsDto>>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdStatsListQueryHandler(AnalyticsDbRepository<AdStatsEntity> adGroupStatsDbRepo)
        {
            AdStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdStatsDto>>> Handle(GetAdStatsListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdStatsEntity>();

            if (request.AdId.HasValue())
                predicate = predicate.And(f => f.AdId == request.AdId);

            if (request.Platform.HasValue())
                predicate = predicate.And(f => f.Platform == request.Platform);

            if (request.CaculatedAt.HasValue())
                predicate = predicate.And(f => f.CaculatedAt == request.CaculatedAt);

            var items = await AdStatsDbRepo.FindAsync(
                selector: AdStatsDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.CaculatedAt),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdStatsDto>>();
            return result.Success(items);
        }
    }
}
