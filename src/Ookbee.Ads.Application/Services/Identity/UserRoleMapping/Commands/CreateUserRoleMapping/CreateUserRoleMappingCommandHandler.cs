using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommandHandler : IRequestHandler<CreateUserRoleMappingCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public CreateUserRoleMappingCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mapper = mapper;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(CreateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.InsertAsync(entity);
            await UserRoleMappingDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
