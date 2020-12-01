using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<UserEntity> UserDbRepo;

        public DeleteUserCommandHandler(
            IMapper mapper,
            AdsDbRepository<UserEntity> userDbRepo)
        {
            Mapper = mapper;
            UserDbRepo = userDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await UserDbRepo.DeleteAsync(request.Id);
            await UserDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
