using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQuery : IRequest<HttpResult<UserRoleDto>>
    {
        public long Id { get; set; }

        public GetUserRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
