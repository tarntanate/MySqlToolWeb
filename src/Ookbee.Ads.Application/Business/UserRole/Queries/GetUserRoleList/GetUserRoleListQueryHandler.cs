using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQueryHandler : IRequestHandler<GetUserRoleListQuery, HttpResult<IEnumerable<UserRoleDto>>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public GetUserRoleListQueryHandler(AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<IEnumerable<UserRoleDto>>> Handle(GetUserRoleListQuery request, CancellationToken cancellationToken)
        {
            var items = await UserRoleDbRepo.FindAsync(
                selector: UserRoleDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<UserRoleDto>>();
            return result.Success(items);
        }
    }
}
