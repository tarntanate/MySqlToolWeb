using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserListByRoleId
{
    public class GetUserListByRoleIdQueryHandler : IRequestHandler<GetUserListByRoleIdQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo;

        public GetUserListByRoleIdQueryHandler(
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetUserListByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<UserRoleMappingEntity>();

            if (request.RoleId.HasValue())
                predicate = predicate.And(f => f.RoleId == request.RoleId);

            var items = await UserRoleMappingDbRepo.FindAsync(
                selector: f => new { f.UserId },
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.RoleId),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<long>>();
            return (items.HasValue())
                ? result.Success(items.Select(x => x.UserId))
                : result.Fail(404, $"Data not found.");
        }
    }
}
