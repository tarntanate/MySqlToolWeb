using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<HttpResult<IEnumerable<UserDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetUserListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
