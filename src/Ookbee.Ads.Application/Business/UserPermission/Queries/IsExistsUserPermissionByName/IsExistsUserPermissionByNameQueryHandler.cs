﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserPermission.Queries.IsExistsUserPermissionByName
{
    public class IsExistsUserPermissionByNameQueryHandler : IRequestHandler<IsExistsUserPermissionByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public IsExistsUserPermissionByNameQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserPermissionByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsUserPermissionByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await UserPermissionDbRepo.AnyAsync(f =>
                f.ExtensionName == request.ExtensionName
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"UserPermission '{request.ExtensionName}' doesn't exist.");
        }
    }
}