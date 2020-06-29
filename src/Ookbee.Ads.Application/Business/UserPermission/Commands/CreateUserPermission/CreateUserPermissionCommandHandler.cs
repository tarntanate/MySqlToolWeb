using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.CreateUserPermission
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
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateUserPermissionCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.InsertAsync(entity);
            await UserPermissionDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
