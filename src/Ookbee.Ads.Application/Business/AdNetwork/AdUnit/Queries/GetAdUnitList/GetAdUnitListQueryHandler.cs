using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQueryHandler : IRequestHandler<GetAdUnitListQuery, HttpResult<IEnumerable<AdUnitDto>>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetAdUnitListQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdUnitDto>>> Handle(GetAdUnitListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdUnitEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            var items = await AdUnitDbRepo.FindAsync(
                selector: AdUnitDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.SortSeq),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdUnitDto>>();
            return result.Success(items);
        }
    }
}
