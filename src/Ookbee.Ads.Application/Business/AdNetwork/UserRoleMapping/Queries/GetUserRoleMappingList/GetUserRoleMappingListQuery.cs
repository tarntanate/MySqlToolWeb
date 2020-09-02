using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQuery : IRequest<HttpResult<IEnumerable<UserRoleMappingDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public long? UserId { get; set; }

        public long? RoleId { get; set; }

        public GetUserRoleMappingListQuery(int start, int length, long? userId, long? roleId)
        {
            Start = start;
            Length = length;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
