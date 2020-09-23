using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommandHandler : IRequestHandler<UpdateUserPermissionCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public UpdateUserPermissionCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mapper = mapper;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.UpdateAsync(entity.Id, entity);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
