﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkByPlatform
{
    public class IsExistsAdNetworkByPlatformQueryHandler : IRequestHandler<IsExistsAdNetworkByPlatformQuery, Response<bool>>
    {
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public IsExistsAdNetworkByPlatformQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdNetworkByPlatformQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdNetworkDbRepo.AnyAsync(f =>
                f.Platform == request.Platform &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Data doesn't exist.");
        }
    }
}
