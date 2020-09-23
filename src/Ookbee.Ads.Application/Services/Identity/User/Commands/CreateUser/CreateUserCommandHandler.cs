using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public CreateUserCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserEntity> authUserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            userDbRepo = authUserDbRepo;
        }

        public async Task<Response<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await userDbRepo.InsertAsync(entity);
            await userDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
