using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommand : UpdateUserPermissionRequest, IRequest<HttpResult<bool>>
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
