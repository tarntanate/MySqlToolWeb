using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQueryHandler : IRequestHandler<IsExistsUserByIdQuery, Response<bool>>
    {
        private AdsDbRepository<UserEntity> UserDbRepo { get; }

        public IsExistsUserByIdQueryHandler(
            AdsDbRepository<UserEntity> userDbRepo)
        {
            UserDbRepo = userDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsUserByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await UserDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Data doesn't exist.");
        }
    }
}
