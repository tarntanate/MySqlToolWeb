using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQueryHandler : IRequestHandler<GetAdAssetByIdQuery, Response<AdAssetDto>>
    {
        private readonly AdsDbRepository<AdAssetEntity> AdAssetDbRepo;

        public GetAdAssetByIdQueryHandler(
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdAssetDto>> Handle(GetAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdAssetDbRepo.FirstAsync(
                selector: AdAssetDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdAssetDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Data not found.");
        }
    }
}
