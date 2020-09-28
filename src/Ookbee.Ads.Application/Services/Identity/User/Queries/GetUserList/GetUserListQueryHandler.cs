using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, Response<IEnumerable<UserDto>>>
    {
        private readonly AdsDbRepository<UserEntity> UserDbRepo;

        public GetUserListQueryHandler(
            AdsDbRepository<UserEntity> userDbRepo)
        {
            UserDbRepo = userDbRepo;
        }

        public async Task<Response<IEnumerable<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var items = await UserDbRepo.FindAsync(
                selector: UserDto.Projection,
                orderBy: f => f.OrderBy(o => o.UserName),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<UserDto>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
