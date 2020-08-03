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

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemList
{
    public class GetAdGroupItemListQueryHandler : IRequestHandler<GetAdGroupItemListQuery, HttpResult<IEnumerable<AdGroupItemDto>>>
    {
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public GetAdGroupItemListQueryHandler(AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdGroupItemDto>>> Handle(GetAdGroupItemListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdGroupItemDto>>> GetListOnDb(GetAdGroupItemListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdGroupItemDto>>();

            var predicate = PredicateBuilder.True<AdGroupItemEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            var items = await AdGroupItemDbRepo.FindAsync(
                selector: AdGroupItemDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.SortSeq),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
