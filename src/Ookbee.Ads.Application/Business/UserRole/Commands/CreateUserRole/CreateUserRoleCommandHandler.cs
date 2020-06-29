using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateUserRoleCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.InsertAsync(entity);
            await UserRoleDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
