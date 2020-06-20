﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQueryHandler : IRequestHandler<IsExistsAdByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public IsExistsAdByIdQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Ad '{request.Id}' doesn't exist.");
        }
    }
}
