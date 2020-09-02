using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.IsExistsUserRoleById
{
    public class IsExistsUserRoleByIdQueryHandler : IRequestHandler<IsExistsUserRoleByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public IsExistsUserRoleByIdQueryHandler(AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserRoleDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"UserRole '{request.Id}' doesn't exist.");
        }
    }
}
