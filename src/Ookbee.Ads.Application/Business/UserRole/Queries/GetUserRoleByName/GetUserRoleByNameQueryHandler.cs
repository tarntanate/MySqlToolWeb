using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQueryHandler : IRequestHandler<GetUserRoleByNameQuery, HttpResult<UserRoleDto>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public GetUserRoleByNameQueryHandler(AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<UserRoleDto>> Handle(GetUserRoleByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<UserRoleDto>> GetOnDb(GetUserRoleByNameQuery request)
        {
            var result = new HttpResult<UserRoleDto>();

            var item = await UserRoleDbRepo.FirstAsync(
                selector: UserRoleDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserRole '{request.Name}' doesn't exist.");
        }
    }
}
