using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserListByRoleId
{
    public class GetUserListByRoleIdQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? RoleId { get; private set; }

        public GetUserListByRoleIdQuery(int start, int length, long? roleId)
        {
            Start = start;
            Length = length;
            RoleId = roleId;
        }
    }
}
