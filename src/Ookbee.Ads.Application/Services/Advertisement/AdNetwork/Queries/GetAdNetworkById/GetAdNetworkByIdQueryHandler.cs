﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQueryHandler : IRequestHandler<GetAdNetworkByIdQuery, Response<AdNetworkDto>>
    {
        private readonly AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo;

        public GetAdNetworkByIdQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<AdNetworkDto>> Handle(GetAdNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdNetworkDbRepo.FirstAsync(
                selector: AdNetworkDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdNetworkDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}