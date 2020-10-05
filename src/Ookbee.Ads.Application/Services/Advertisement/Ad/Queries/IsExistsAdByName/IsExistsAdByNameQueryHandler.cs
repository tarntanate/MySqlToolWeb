﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryHandler : IRequestHandler<IsExistsAdByNameQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public IsExistsAdByNameQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}