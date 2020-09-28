using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQuery : IRequest<Response<IEnumerable<UserRoleMappingDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? UserId { get; private set; }
        public long? RoleId { get; private set; }

        public GetUserRoleMappingListQuery(int start, int length, long? userId, long? roleId)
        {
            Start = start;
            Length = length;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
