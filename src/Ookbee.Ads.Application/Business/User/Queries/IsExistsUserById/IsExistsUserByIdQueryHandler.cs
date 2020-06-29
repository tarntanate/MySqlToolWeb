using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQueryHandler : IRequestHandler<IsExistsUserByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public IsExistsUserByIdQueryHandler(AdsDbRepository<UserEntity> authUserDbRepo)
        {
            userDbRepo = authUserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsUserByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await userDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"User '{request.Id}' doesn't exist.");
        }
    }
}
