using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommandHandler : IRequestHandler<UpdateUserRoleMappingCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public UpdateUserRoleMappingCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.UpdateAsync(entity.Id, entity);
            await UserRoleMappingDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
