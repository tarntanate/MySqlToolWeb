﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQueryHandler : IRequestHandler<IsExistsAdGroupByNameQuery, Response<bool>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public IsExistsAdGroupByNameQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdGroupByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            if (!isExists)
                return result.Fail(404, $"AdGroup '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
