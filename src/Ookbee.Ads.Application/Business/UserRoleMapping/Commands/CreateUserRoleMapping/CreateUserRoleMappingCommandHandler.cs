using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommandHandler : IRequestHandler<CreateUserRoleMappingCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public CreateUserRoleMappingCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(CreateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> CreateOnDb(CreateUserRoleMappingCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.InsertAsync(entity);
            await UserRoleMappingDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
