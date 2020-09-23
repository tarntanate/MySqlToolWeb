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
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public UpdateUserCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserEntity> authUserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            userDbRepo = authUserDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await userDbRepo.UpdateAsync(entity.Id, entity);
            await userDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
