using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQueryHandler : IRequestHandler<GetUserRoleListQuery, Response<IEnumerable<UserRoleDto>>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public GetUserRoleListQueryHandler(
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<IEnumerable<UserRoleDto>>> Handle(GetUserRoleListQuery request, CancellationToken cancellationToken)
        {
            var items = await UserRoleDbRepo.FindAsync(
                selector: UserRoleDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<UserRoleDto>>();
            return (items.HasValue())
                ? result.Success(items)
                : result.Fail(404, $"Data not found.");
        }
    }
}
