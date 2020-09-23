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
        private IMediator Mediator { get; }
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public DeleteUserPermissionCommandHandler(
            IMediator mediator,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mediator = mediator;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
        {
            await UserPermissionDbRepo.DeleteAsync(request.Id);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
