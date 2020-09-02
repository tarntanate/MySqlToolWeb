using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommand : UpdateUserRoleMappingRequest, IRequest<HttpResult<bool>>
    {
        public UpdateUserRoleMappingCommand(UpdateUserRoleMappingRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
