using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<HttpResult<UserDto>>
    {
        public long Id { get; set; }

        public GetUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
