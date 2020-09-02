using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommand : CreateUserRoleMappingRequest, IRequest<HttpResult<bool>>
    {
        public CreateUserRoleMappingCommand(CreateUserRoleMappingRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
