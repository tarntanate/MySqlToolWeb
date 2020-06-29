using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommand : UpdateUserRoleMappingRequest, IRequest<HttpResult<bool>>
    {
        public UpdateUserRoleMappingCommand(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
