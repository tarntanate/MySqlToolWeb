using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await userDbRepo.UpdateAsync(entity.Id, entity);
            await userDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
