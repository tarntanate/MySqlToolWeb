using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQueryHandler : IRequestHandler<GetAdAssetByIdQuery, HttpResult<AdAssetDto>>
    {
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public GetAdAssetByIdQueryHandler(AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<AdAssetDto>> Handle(GetAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdAssetDto>> GetOnDb(GetAdAssetByIdQuery request)
        {
            var result = new HttpResult<AdAssetDto>();

            var item = await AdAssetEFCoreRepo.FirstAsync(filter: f => f.Id == request.Id && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdAsset '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<AdAssetDto>();

            return result.Success(data);
        }
    }
}
