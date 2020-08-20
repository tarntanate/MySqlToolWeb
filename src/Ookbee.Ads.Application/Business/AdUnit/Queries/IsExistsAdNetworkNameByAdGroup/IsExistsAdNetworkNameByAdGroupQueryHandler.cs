﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdNetworkNameByAdGroup
{
    public class IsExistsAdNetworkNameByAdGroupQueryHandler : IRequestHandler<IsExistsAdNetworkNameByAdGroupQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public IsExistsAdNetworkNameByAdGroupQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdNetworkNameByAdGroupQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdNetworkNameByAdGroupQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdNetwork == request.AdNetworkName &&
                f.AdGroupId == request.AdGroupId &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Ad Network '{request.AdNetworkName}' Unit is exist in group id {request.AdGroupId}.");
        }
    }
}