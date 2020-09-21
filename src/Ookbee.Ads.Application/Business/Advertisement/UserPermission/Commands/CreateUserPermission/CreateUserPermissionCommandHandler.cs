using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommandHandler : IRequestHandler<CreateUserPermissionCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public CreateUserPermissionCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.InsertAsync(entity);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
