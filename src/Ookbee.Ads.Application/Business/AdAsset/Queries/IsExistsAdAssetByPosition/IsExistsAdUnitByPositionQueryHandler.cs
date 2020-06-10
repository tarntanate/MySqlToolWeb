using MediatR;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQueryHandler : IRequestHandler<IsExistsAdAssetByPositionQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public IsExistsAdAssetByPositionQueryHandler(AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdAssetByPositionQuery request)
        {
            var result = new HttpResult<bool>();

            var position = Enum.GetName(typeof(Position), request.Position);
            var isExists = await AdAssetEFCoreRepo.AnyAsync(f => f.Position == position);

            if (!isExists)
                return result.Fail(404, $"AdAsset '{position}' doesn't exist.");
            return result.Success(true);
        }
    }
}
