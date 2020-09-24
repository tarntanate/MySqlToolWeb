using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long RoleId { get; private set; }
        public string ExtensionName { get; private set; }
        public bool IsCreate { get; private set; }
        public bool IsRead { get; private set; }
        public bool IsUpdate { get; private set; }
        public bool IsDelete { get; private set; }

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
