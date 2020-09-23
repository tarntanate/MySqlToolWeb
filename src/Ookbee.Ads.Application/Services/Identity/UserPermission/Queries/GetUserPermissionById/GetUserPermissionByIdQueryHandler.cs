using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQueryHandler : IRequestHandler<GetUserPermissionByIdQuery, Response<UserPermissionDto>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public GetUserPermissionByIdQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<UserPermissionDto>> Handle(GetUserPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await UserPermissionDbRepo.FirstAsync(
                selector: UserPermissionDto.Projection,
                filter: f => f.Id == request.Id
            );

            var result = new Response<UserPermissionDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserPermission '{request.Id}' doesn't exist.");
        }
    }
}
