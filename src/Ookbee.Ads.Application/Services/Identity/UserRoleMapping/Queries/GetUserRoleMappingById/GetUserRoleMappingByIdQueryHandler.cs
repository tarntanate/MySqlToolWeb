using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingById
{
    public class GetUserRoleMappingByIdQueryHandler : IRequestHandler<GetUserRoleMappingByIdQuery, Response<UserRoleMappingDto>>
    {
        private readonly AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo;

        public GetUserRoleMappingByIdQueryHandler(
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<UserRoleMappingDto>> Handle(GetUserRoleMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await UserRoleMappingDbRepo.FirstAsync(
                selector: UserRoleMappingDto.Projection,
                filter: f =>
                    f.UserId == request.UserId &&
                    f.RoleId == request.RoleId
            );

            var result = new Response<UserRoleMappingDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
