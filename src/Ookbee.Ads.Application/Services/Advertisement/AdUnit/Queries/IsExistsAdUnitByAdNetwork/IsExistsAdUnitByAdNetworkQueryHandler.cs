﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdNetwork
{
    public class IsExistsAdUnitByAdNetworkQueryHandler : IRequestHandler<IsExistsAdUnitByAdNetworkQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public IsExistsAdUnitByAdNetworkQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitByAdNetworkQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdNetwork == request.AdNetwork &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
