using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommandHandler : IRequestHandler<DeleteUserRoleMappingCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public DeleteUserRoleMappingCommandHandler(
            IMediator mediator,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mediator = mediator;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            await UserRoleMappingDbRepo.DeleteAsync(new { request.UserId, request.RoleId });
            await UserRoleMappingDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
