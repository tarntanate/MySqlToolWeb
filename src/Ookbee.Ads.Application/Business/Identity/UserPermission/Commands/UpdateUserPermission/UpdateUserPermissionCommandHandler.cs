using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommandHandler : IRequestHandler<UpdateUserPermissionCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public UpdateUserPermissionCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.UpdateAsync(entity.Id, entity);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
