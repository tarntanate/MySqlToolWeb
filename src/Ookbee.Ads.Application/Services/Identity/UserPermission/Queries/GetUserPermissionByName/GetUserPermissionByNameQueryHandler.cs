using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQueryHandler : IRequestHandler<GetUserPermissionByNameQuery, Response<UserPermissionDto>>
    {
        private readonly AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo;

        public GetUserPermissionByNameQueryHandler(
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<UserPermissionDto>> Handle(GetUserPermissionByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await UserPermissionDbRepo.FirstAsync(
                selector: UserPermissionDto.Projection,
                filter: f => f.ExtensionName == request.ExtensionName
            );

            var result = new Response<UserPermissionDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
