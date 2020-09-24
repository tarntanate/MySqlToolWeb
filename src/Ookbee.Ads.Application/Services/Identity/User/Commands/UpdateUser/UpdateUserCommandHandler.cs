using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserEntity> UserDbRepo;

        public UpdateUserCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserEntity> userDbRepo)
        {
            Mapper = mapper;
            UserDbRepo = userDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await UserDbRepo.UpdateAsync(entity.Id, entity);
            await UserDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
