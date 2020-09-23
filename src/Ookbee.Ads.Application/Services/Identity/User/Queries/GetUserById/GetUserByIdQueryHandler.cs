using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public GetUserByIdQueryHandler(AdsDbRepository<UserEntity> authUserDbRepo)
        {
            userDbRepo = authUserDbRepo;
        }

        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await userDbRepo.FirstAsync(
                selector: UserDto.Projection,
                filter: f =>
                    f.Id == request.Id
            );

            var result = new Response<UserDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"User '{request.Id}' doesn't exist.");
        }
    }
}
