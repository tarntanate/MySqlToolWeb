using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommand : CreateUserPermissionRequest, IRequest<Response<long>>
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
