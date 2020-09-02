using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.IsExistsUserPermissionById
{
    public class IsExistsUserPermissionByIdQueryHandler : IRequestHandler<IsExistsUserPermissionByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public IsExistsUserPermissionByIdQueryHandler(AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserPermissionDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"UserPermission '{request.Id}' doesn't exist.");
        }
    }
}
