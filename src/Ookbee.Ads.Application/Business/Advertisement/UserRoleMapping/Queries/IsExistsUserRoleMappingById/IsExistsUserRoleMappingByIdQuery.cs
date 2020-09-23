using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Queries.IsExistsUserRoleMappingById
{
    public class IsExistsUserRoleMappingByIdQuery : IRequest<Response<bool>>
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
