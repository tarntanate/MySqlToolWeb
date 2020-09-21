using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommand : CreateUserRoleRequest, IRequest<HttpResult<bool>>
    {
        public CreateUserRoleMappingCommand(CreateUserRoleRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
