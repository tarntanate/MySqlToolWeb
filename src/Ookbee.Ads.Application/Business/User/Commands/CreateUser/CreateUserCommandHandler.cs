using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateUserCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<UserEntity>(request);
            await userDbRepo.InsertAsync(entity);
            await userDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
