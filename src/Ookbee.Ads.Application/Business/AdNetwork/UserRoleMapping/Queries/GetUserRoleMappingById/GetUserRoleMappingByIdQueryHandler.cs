using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Queries.GetUserRoleMappingById
{
    public class GetUserRoleMappingByIdQueryHandler : IRequestHandler<GetUserRoleMappingByIdQuery, HttpResult<UserRoleMappingDto>>
    {
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public GetUserRoleMappingByIdQueryHandler(AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<HttpResult<UserRoleMappingDto>> Handle(GetUserRoleMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await UserRoleMappingDbRepo.FirstAsync(
                selector: UserRoleMappingDto.Projection,
                filter: f =>
                    f.UserId == request.UserId &&
                    f.RoleId == request.RoleId
            );

            var result = new HttpResult<UserRoleMappingDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserRoleMapping 'UserId:{request.UserId} and RoleId:{request.UserId}' doesn't exist.");
        }
    }
}
