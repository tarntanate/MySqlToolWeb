using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetByAdId
{
    public class GetAdAssetByAdIdQueryHandler : IRequestHandler<GetAdAssetByAdIdQuery, Response<IEnumerable<AdAssetDto>>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GetAdAssetByAdIdQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<IEnumerable<AdAssetDto>>> Handle(GetAdAssetByAdIdQuery request, CancellationToken cancellationToken)
        {
            var items = await AdAssetDbRepo.FindAsync(
                selector: AdAssetDto.Projection,
                filter: f =>
                    f.Id == request.AdId &&
                    f.DeletedAt == null
            );

            var result = new Response<IEnumerable<AdAssetDto>>();
            return (items != null)
                ? result.Success(items)
                : result.Fail(404, $"AdAsset By Ad'{request.AdId}' doesn't exist.");
        }
    }
}
