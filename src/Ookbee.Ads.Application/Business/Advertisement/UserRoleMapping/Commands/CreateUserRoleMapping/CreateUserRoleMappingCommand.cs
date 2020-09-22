using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommand : CreateUserRoleMappingRequest, IRequest<Response<bool>>
    {
        public CreateUserRoleMappingCommand(CreateUserRoleMappingRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
