using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByRoleId
{
    public class GetUserIdListByRoleIdQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? RoleId { get; private set; }

        public GetUserIdListByRoleIdQuery(int start, int length, long? roleId)
        {
            Start = start;
            Length = length;
            RoleId = roleId;
        }
    }
}
