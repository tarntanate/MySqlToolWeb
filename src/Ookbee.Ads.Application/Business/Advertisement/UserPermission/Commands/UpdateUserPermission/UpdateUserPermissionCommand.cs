using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommand : UpdateUserPermissionRequest, IRequest<Response<bool>>
    {
        public UpdateUserPermissionCommand(long id, UpdateUserPermissionRequest request)
        {
            Id = id;
            RoleId = request.RoleId;
            ExtensionName = request.ExtensionName;
            IsCreate = request.IsCreate;
            IsRead = request.IsRead;
            IsUpdate = request.IsUpdate;
            IsDelete = request.IsDelete;
        }
    }
}
