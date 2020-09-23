using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQueryHandler : IRequestHandler<IsExistsUserByIdQuery, Response<bool>>
    {
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public IsExistsUserByIdQueryHandler(AdsDbRepository<UserEntity> authUserDbRepo)
        {
            userDbRepo = authUserDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsUserByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await userDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"User '{request.Id}' doesn't exist.");
        }
    }
}
