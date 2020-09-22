using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQuery : IRequest<Response<IEnumerable<UserRoleDto>>>
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
