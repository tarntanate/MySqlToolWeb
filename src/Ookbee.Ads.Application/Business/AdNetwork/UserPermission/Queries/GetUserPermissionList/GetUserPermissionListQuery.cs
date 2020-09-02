using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.GetUserPermissionList
{
    public class GetUserPermissionListQuery : IRequest<HttpResult<IEnumerable<UserPermissionDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetUserPermissionListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
