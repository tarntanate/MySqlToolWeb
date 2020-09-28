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

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQueryHandler : IRequestHandler<GetUserRoleMappingListQuery, Response<IEnumerable<UserRoleMappingDto>>>
    {
        private readonly AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo;

        public GetUserRoleMappingListQueryHandler(
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<IEnumerable<UserRoleMappingDto>>> Handle(GetUserRoleMappingListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<UserRoleMappingEntity>();

            if (request.UserId.HasValue())
                predicate = predicate.And(f => f.UserId == request.UserId);

            if (request.RoleId.HasValue())
                predicate = predicate.And(f => f.RoleId == request.RoleId);

            var items = await UserRoleMappingDbRepo.FindAsync(
                selector: UserRoleMappingDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.UserId),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<UserRoleMappingDto>>();
            return result.Success(items);
        }
    }
}
