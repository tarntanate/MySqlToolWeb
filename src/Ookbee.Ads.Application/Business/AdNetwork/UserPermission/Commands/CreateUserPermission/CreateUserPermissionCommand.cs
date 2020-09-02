using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommand : CreateUserPermissionRequest, IRequest<HttpResult<long>>
    {
        public CreateUserPermissionCommand(CreateUserPermissionRequest request)
        {
            RoleId = request.RoleId;
            ExtensionName = request.ExtensionName;
            IsCreate = request.IsCreate;
            IsRead = request.IsRead;
            IsUpdate = request.IsUpdate;
            IsDelete = request.IsDelete;
        }
    }
}
