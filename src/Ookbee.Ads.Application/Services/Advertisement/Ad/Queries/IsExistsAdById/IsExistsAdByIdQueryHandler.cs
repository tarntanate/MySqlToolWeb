﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQueryHandler : IRequestHandler<IsExistsAdByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public IsExistsAdByIdQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}