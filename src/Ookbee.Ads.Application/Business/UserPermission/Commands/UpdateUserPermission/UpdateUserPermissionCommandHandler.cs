using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommandHandler : IRequestHandler<UpdateUserPermissionCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.UpdateAsync(entity.Id, entity);
            await UserPermissionDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
