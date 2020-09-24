using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.IsExistsUserPermissionByName
{
    public class IsExistsUserPermissionByNameQueryHandler : IRequestHandler<IsExistsUserPermissionByNameQuery, Response<bool>>
    {
        private readonly AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo;

        public IsExistsUserPermissionByNameQueryHandler(
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsUserPermissionByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserPermissionDbRepo.AnyAsync(f =>
                f.ExtensionName == request.ExtensionName
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
