using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public DeleteUserRoleCommandHandler(
            IMediator mediator,
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            Mediator = mediator;
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteUserRoleCommand request)
        {
            var result = new HttpResult<bool>();

            await UserRoleDbRepo.DeleteAsync(request.Id);
            await UserRoleDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
