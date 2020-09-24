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
        private AdsDbRepository<UserEntity> UserDbRepor { get; }

        public UpdateUserCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserEntity> userDbRepor)
        {
            Mapper = mapper;
            UserDbRepor = userDbRepor;
        }

        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request);
            await UserDbRepor.UpdateAsync(entity.Id, entity);
            await UserDbRepor.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
