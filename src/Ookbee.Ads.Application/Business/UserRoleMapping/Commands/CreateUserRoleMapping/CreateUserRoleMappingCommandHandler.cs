using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommandHandler : IRequestHandler<CreateUserRoleMappingCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateUserRoleMappingCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.InsertAsync(entity);
            await UserRoleMappingDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
