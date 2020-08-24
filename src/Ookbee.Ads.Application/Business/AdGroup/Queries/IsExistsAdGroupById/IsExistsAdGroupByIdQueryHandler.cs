﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQueryHandler : IRequestHandler<IsExistsAdGroupByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public IsExistsAdGroupByIdQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new HttpResult<bool>();
            if (!isExists)
                return result.Fail(404, $"AdGroup '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
