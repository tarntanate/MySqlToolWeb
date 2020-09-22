using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.UpdateAsync(entity.Id, entity);
            await UserRoleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
