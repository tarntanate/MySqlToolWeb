﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroup
{
    public class IsExistsAdUnitByAdGroupQueryHandler : IRequestHandler<IsExistsAdUnitByAdGroupQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public IsExistsAdUnitByAdGroupQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitByAdGroupQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdNetwork == request.AdNetworkName &&
                f.AdGroupId == request.AdGroupId &&
                f.DeletedAt == null
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Ad Network '{request.AdNetworkName}' Unit is exist in group id {request.AdGroupId}.");
        }
    }
}