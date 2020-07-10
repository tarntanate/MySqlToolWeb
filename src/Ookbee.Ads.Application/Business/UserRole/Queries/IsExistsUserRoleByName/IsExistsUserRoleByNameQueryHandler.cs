﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQueryHandler : IRequestHandler<IsExistsUserRoleByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public IsExistsUserRoleByNameQueryHandler(AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserRoleByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsUserRoleByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await UserRoleDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"UserRole '{request.Name}' doesn't exist.");
        }
    }
}