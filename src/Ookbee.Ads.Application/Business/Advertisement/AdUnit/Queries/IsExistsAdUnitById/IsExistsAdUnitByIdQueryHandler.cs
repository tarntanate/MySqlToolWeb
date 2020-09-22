﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQueryHandler : IRequestHandler<IsExistsAdUnitByIdQuery, Response<bool>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public IsExistsAdUnitByIdQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdUnit '{request.Id}' doesn't exist.");
        }
    }
}
