using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<Response<IEnumerable<UserDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }

        public GetUserListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
