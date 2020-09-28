using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQuery : IRequest<Response<UserRoleDto>>
    {
        public long Id { get; private set; }

        public GetUserRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
