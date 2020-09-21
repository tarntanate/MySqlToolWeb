using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQueryHandler : IRequestHandler<GetUserPermissionByNameQuery, HttpResult<UserPermissionDto>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public GetUserPermissionByNameQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<UserPermissionDto>> Handle(GetUserPermissionByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await UserPermissionDbRepo.FirstAsync(
                selector: UserPermissionDto.Projection,
                filter: f => f.ExtensionName == request.ExtensionName
            );

            var result = new HttpResult<UserPermissionDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserPermission '{request.ExtensionName}' doesn't exist.");
        }
    }
}
