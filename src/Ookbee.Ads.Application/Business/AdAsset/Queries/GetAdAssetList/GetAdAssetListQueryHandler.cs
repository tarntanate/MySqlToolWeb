using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQueryHandler : IRequestHandler<GetAdAssetListQuery, HttpResult<IEnumerable<AdAssetDto>>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GetAdAssetListQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdAssetDto>>> Handle(GetAdAssetListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdAssetEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdId.HasValue() && request.AdId > 0)
                predicate = predicate.And(f => f.AdId == request.AdId);

            var items = await AdAssetDbRepo.FindAsync(
                selector: AdAssetDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.AdId).ThenBy(o => o.Position),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdAssetDto>>();
            return result.Success(items);
        }
    }
}
