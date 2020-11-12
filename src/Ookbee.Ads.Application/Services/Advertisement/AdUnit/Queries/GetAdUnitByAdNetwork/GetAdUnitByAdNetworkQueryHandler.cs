﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQueryHandler : IRequestHandler<GetAdUnitByAdNetworkQuery, Response<AdUnitDto>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public GetAdUnitByAdNetworkQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdUnitDto>> Handle(GetAdUnitByAdNetworkQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitDbRepo.FirstAsync<AdUnitDto>(
                filter: f =>
                    f.AdNetwork == request.AdNetwork &&
                    f.DeletedAt == null
            );

            var result = new Response<AdUnitDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
