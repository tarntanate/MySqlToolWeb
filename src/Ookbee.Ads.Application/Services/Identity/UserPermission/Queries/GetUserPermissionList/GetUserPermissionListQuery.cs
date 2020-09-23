using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionList
{
    public class GetUserPermissionListQuery : IRequest<Response<IEnumerable<UserPermissionDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }
        public long? RoleId { get; set; }

        public GetUserPermissionListQuery(int start, int length, long? roleId)
        {
            Start = start;
            Length = length;
            RoleId = roleId;
        }
    }
}
