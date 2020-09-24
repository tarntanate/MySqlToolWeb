using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQueryHandler : IRequestHandler<IsExistsUserRoleByNameQuery, Response<bool>>
    {
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public IsExistsUserRoleByNameQueryHandler(
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsUserRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserRoleDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Data doesn't exist.");
        }
    }
}
