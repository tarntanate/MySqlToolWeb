using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingById
{
    public class GetUserRoleMappingByIdQuery : IRequest<Response<UserRoleMappingDto>>
    {
        public long UserId { get; private set; }
        public long RoleId { get; private set; }

        public GetUserRoleMappingByIdQuery(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
