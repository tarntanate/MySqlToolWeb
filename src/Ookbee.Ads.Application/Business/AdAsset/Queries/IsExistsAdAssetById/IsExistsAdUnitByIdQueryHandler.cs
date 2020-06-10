﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQueryHandler : IRequestHandler<IsExistsAdAssetByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public IsExistsAdAssetByIdQueryHandler(AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdAssetByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdAssetEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"AdAsset '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
