using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommand : CreateUserPermissionRequest, IRequest<HttpResult<long>>
    {
        public CreateUserPermissionCommand(CreateUserPermissionRequest request)
        {
            RoleId = request.RoleId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
