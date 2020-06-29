using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Queries.IsExistsUserRoleMappingById
{
    public class IsExistsUserRoleMappingByIdQueryHandler : IRequestHandler<IsExistsUserRoleMappingByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public IsExistsUserRoleMappingByIdQueryHandler(AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserRoleMappingByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsUserRoleMappingByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await UserRoleMappingDbRepo.AnyAsync(f =>
                f.UserId == request.UserId &&
                f.RoleId == request.RoleId
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"UserRoleMapping 'UserId:{request.UserId} and RoleId:{request.UserId}' doesn't exist.");
        }
    }
}
