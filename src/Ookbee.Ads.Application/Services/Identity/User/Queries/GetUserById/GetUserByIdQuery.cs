using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<UserDto>>
    {
        public long Id { get; private set; }

        public GetUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
