using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleEntity> UserRoleDbRepo { get; }

        public CreateUserRoleCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<long>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.InsertAsync(entity);
            await UserRoleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
