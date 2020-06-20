﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
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

            var isExists = await AdAssetDbRepo.AnyAsync(f =>
                f.AdId == request.AdId &&
                f.Position == request.Position &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdAsset '{request.Position.ToString()}' doesn't exist.");
        }
    }
}
