﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQueryHandler : IRequestHandler<IsExistsAdUnitTypeByIdQuery, Response<bool>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public IsExistsAdUnitTypeByIdQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitTypeDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            if (!isExists)
                return result.Fail(404, $"AdUnitType '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
