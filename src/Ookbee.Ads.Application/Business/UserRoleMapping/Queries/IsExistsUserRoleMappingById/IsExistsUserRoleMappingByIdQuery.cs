using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Queries.IsExistsUserRoleMappingById
{
    public class IsExistsUserRoleMappingByIdQuery : IRequest<HttpResult<bool>>
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public IsExistsUserRoleMappingByIdQuery(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
