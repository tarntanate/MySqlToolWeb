using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQueryHandler : IRequestHandler<GetUserRoleByNameQuery, Response<UserRoleDto>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public GetUserRoleByNameQueryHandler(
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<UserRoleDto>> Handle(GetUserRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await UserRoleDbRepo.FirstAsync(
                selector: UserRoleDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            var result = new Response<UserRoleDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserRole '{request.Name}' doesn't exist.");
        }
    }
}
