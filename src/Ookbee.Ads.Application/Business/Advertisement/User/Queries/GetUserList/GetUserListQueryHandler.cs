using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.User.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, HttpResult<IEnumerable<UserDto>>>
    {
        private AdsDbRepository<UserEntity> UserDbRepo { get; }

        public GetUserListQueryHandler(AdsDbRepository<UserEntity> userDbRepo)
        {
            UserDbRepo = userDbRepo;
        }

        public async Task<HttpResult<IEnumerable<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var items = await UserDbRepo.FindAsync(
                selector: UserDto.Projection,
                orderBy: f => f.OrderBy(o => o.UserName),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<UserDto>>();
            return result.Success(items);
        }
    }
}
