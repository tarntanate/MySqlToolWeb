using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQuery : IRequest<HttpResult<IEnumerable<UserRoleDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetUserRoleListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
