using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserRoleEntity> UserRoleDbRepo;

        public CreateUserRoleCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            Mapper = mapper;
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<long>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.InsertAsync(entity);
            await UserRoleDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
