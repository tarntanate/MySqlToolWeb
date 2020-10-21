using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByPermissionName
{
    public class GetUserIdListByPermissionNameQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public string PermissionName { get; private set; }

        public GetUserIdListByPermissionNameQuery(int start, int length, string permissionName)
        {
            Start = start;
            Length = length;
            PermissionName = permissionName;
        }
    }
}
