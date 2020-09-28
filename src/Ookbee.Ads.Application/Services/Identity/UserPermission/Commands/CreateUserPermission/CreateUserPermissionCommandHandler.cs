using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommandHandler : IRequestHandler<CreateUserPermissionCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo;

        public CreateUserPermissionCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mapper = mapper;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<Response<long>> Handle(CreateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserPermissionEntity>(request);
            await UserPermissionDbRepo.InsertAsync(entity);
            await UserPermissionDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().OK(entity.Id);
        }
    }
}
