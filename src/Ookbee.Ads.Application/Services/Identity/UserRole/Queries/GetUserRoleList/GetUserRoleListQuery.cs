using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQuery : IRequest<Response<IEnumerable<UserRoleDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }

        public GetUserRoleListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
