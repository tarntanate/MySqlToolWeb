﻿using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.GetUserPermissionList
{
    public class GetUserPermissionListQueryHandler : IRequestHandler<GetUserPermissionListQuery, Response<IEnumerable<UserPermissionDto>>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public GetUserPermissionListQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<IEnumerable<UserPermissionDto>>> Handle(GetUserPermissionListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<UserPermissionEntity>();

            if (request.RoleId.HasValue)
                predicate = predicate.And(f => f.RoleId == request.RoleId);

            var items = await UserPermissionDbRepo.FindAsync(
                selector: UserPermissionDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.ExtensionName),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<UserPermissionDto>>();
            return result.Success(items);
        }
    }
}
