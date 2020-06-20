using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQueryHandler : IRequestHandler<GetAdAssetByIdQuery, HttpResult<AdAssetDto>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GetAdAssetByIdQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<AdAssetDto>> Handle(GetAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdAssetDto>> GetOnDb(GetAdAssetByIdQuery request)
        {
            var result = new HttpResult<AdAssetDto>();

            var item = await AdAssetDbRepo.FirstAsync(
                selector: AdAssetDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdAsset '{request.Id}' doesn't exist.");
        }
    }
}
