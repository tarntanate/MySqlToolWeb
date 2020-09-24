using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommandHandler : IRequestHandler<UpdateUserRoleMappingCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo;

        public UpdateUserRoleMappingCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mapper = mapper;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.UpdateAsync(new { request.UserId, request.RoleId }, entity);
            await UserRoleMappingDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
