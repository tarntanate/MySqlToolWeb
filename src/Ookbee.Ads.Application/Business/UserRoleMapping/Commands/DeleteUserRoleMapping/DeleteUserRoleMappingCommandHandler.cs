using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommandHandler : IRequestHandler<DeleteUserRoleMappingCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(DeleteUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteUserRoleMappingCommand request)
        {
            var result = new HttpResult<bool>();

            await UserRoleMappingDbRepo.DeleteAsync(new { request.UserId, request.RoleId });
            await UserRoleMappingDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
