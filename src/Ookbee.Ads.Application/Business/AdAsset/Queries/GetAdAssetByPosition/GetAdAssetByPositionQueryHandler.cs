using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQueryHandler : IRequestHandler<GetAdAssetByPositionQuery, HttpResult<AdAssetDto>>
    {
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public GetAdAssetByPositionQueryHandler(AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<AdAssetDto>> Handle(GetAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdAssetDto>> GetOnDb(GetAdAssetByPositionQuery request)
        {
            var result = new HttpResult<AdAssetDto>();

            var position = request.Position.ToString();
            var item = await AdAssetEFCoreRepo.FirstAsync(filter: f => f.Position == position);
            if (item == null)
                return result.Fail(404, $"AdAsset '{position}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<AdAssetDto>();

            return result.Success(data);
        }
    }
}
