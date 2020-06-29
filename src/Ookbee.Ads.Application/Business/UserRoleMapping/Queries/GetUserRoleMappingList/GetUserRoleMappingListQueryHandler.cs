using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQueryHandler : IRequestHandler<GetUserRoleMappingListQuery, HttpResult<IEnumerable<UserRoleMappingDto>>>
    {
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public GetUserRoleMappingListQueryHandler(AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<HttpResult<IEnumerable<UserRoleMappingDto>>> Handle(GetUserRoleMappingListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<UserRoleMappingDto>>> GetListOnDb(GetUserRoleMappingListQuery request)
        {
            var result = new HttpResult<IEnumerable<UserRoleMappingDto>>();

            var items = await UserRoleMappingDbRepo.FindAsync(
                selector: UserRoleMappingDto.Projection,
                orderBy: f => f.OrderBy(o => o.UserId),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
