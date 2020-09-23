using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommandHandler : IRequestHandler<DeleteUserRoleMappingCommand, Response<bool>>
    {
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public DeleteUserRoleMappingCommandHandler(
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            await UserRoleMappingDbRepo.DeleteAsync(new { request.UserId, request.RoleId });
            await UserRoleMappingDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
