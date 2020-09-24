using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.IsExistsUserRoleMappingById
{
    public class IsExistsUserRoleMappingByIdQueryHandler : IRequestHandler<IsExistsUserRoleMappingByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo;

        public IsExistsUserRoleMappingByIdQueryHandler(
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsUserRoleMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserRoleMappingDbRepo.AnyAsync(f =>
                f.UserId == request.UserId &&
                f.RoleId == request.RoleId
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
