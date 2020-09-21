using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQueryHandler : IRequestHandler<GetUserPermissionByIdQuery, HttpResult<UserPermissionDto>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public GetUserPermissionByIdQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<UserPermissionDto>> Handle(GetUserPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await UserPermissionDbRepo.FirstAsync(
                selector: UserPermissionDto.Projection,
                filter: f => f.Id == request.Id
            );

            var result = new HttpResult<UserPermissionDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"UserPermission '{request.Id}' doesn't exist.");
        }
    }
}
