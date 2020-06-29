using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public UpdateUserRoleCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.UpdateAsync(entity.Id, entity);
            await UserRoleDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
