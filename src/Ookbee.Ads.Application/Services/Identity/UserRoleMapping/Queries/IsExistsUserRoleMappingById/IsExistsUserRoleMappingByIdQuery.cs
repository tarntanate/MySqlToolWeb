using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.IsExistsUserRoleMappingById
{
    public class IsExistsUserRoleMappingByIdQuery : IRequest<Response<bool>>
    {
        public long UserId { get; private set; }
        public long RoleId { get; private set; }

        public IsExistsUserRoleMappingByIdQuery(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
