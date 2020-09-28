using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserRoleEntity> UserRoleDbRepo;

        public UpdateUserRoleCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserRoleEntity> userRoleDbRepo)
        {
            Mapper = mapper;
            UserRoleDbRepo = userRoleDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleEntity>(request);
            await UserRoleDbRepo.UpdateAsync(entity.Id, entity);
            await UserRoleDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
