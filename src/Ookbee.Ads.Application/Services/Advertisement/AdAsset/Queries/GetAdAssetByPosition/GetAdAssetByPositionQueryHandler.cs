using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQueryHandler : IRequestHandler<GetAdAssetByPositionQuery, Response<AdAssetDto>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GetAdAssetByPositionQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdAssetDto>> Handle(GetAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await AdAssetDbRepo.FirstAsync(
                selector: AdAssetDto.Projection,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.Position == request.Position &&
                    f.DeletedAt == null
            );

            var result = new Response<AdAssetDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdAsset '{request.Position.ToString()}' doesn't exist.");
        }
    }
}
