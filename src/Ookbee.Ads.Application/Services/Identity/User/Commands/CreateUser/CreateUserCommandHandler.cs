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
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserEntity> userDbRepo;

        public CreateUserCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserEntity> authUserDbRepo)
        {
            Mapper = mapper;
            userDbRepo = authUserDbRepo;
        }

        public async Task<Response<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await userDbRepo.InsertAsync(entity);
            await userDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().OK(entity.Id);
        }
    }
}
