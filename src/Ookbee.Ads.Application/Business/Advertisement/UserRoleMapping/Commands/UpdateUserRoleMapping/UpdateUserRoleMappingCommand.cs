using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommand : UpdateUserRoleRequest, IRequest<HttpResult<bool>>
    {
        public UpdateUserRoleMappingCommand(UpdateUserRoleRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
