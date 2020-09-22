using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.User.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<Response<IEnumerable<UserDto>>>
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
