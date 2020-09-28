using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommandHandler : IRequestHandler<DeleteUserPermissionCommand, Response<bool>>
    {
        private readonly AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo;

        public DeleteUserPermissionCommandHandler(
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
        {
            await UserPermissionDbRepo.DeleteAsync(request.Id);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
