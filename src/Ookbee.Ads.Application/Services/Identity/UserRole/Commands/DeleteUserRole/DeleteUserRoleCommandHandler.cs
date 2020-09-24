using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, Response<bool>>
    {
        private readonly AdsDbRepository<UserRoleEntity> UserRoleDbRepo;

        public DeleteUserRoleCommandHandler(
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            await UserRoleDbRepo.DeleteAsync(request.Id);
            await UserRoleDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
