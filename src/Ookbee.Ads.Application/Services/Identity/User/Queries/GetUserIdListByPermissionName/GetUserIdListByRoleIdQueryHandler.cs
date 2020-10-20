﻿using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByPermissionName
{
    public class GetUserIdListByPermissionNameQueryHandler : IRequestHandler<GetUserIdListByPermissionNameQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<UserEntity> UserDbRepo;

        public GetUserIdListByPermissionNameQueryHandler(
            AdsDbRepository<UserEntity> userDbRepo)
        {
            UserDbRepo = userDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetUserIdListByPermissionNameQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<UserEntity>();

            if (request.PermissionName.HasValue())
                predicate = predicate.And(f => f.Role.Permissions.Any(p => p.ExtensionName.ToUpper() == request.PermissionName.ToUpper()));

            var items = await UserDbRepo.FindAsync(
                selector: f => new { f.Id },
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.RoleId),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<long>>();
            return (items.HasValue())
                ? result.OK(items.Select(x => x.Id))
                : result.NotFound();
        }
    }
}
