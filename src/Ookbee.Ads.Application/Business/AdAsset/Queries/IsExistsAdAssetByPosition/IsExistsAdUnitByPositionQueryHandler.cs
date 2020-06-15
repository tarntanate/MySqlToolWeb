using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQueryHandler : IRequestHandler<IsExistsAdAssetByPositionQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public IsExistsAdAssetByPositionQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdAssetByPositionQuery request)
        {
            var result = new HttpResult<bool>();

            var position = Enum.GetName(typeof(Position), request.Position);
            var isExists = await AdAssetDbRepo.AnyAsync(f => f.Position == position);

            if (!isExists)
                return result.Fail(404, $"AdAsset '{position}' doesn't exist.");
            return result.Success(true);
        }
    }
}
