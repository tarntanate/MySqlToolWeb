using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Queries.GetUserRoleMappingById
{
    public class GetUserRoleMappingByIdQuery : IRequest<HttpResult<UserRoleMappingDto>>
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public GetUserRoleMappingByIdQuery(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
