using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteUserPermissionCommand(long id)
        {
            Id = id;
        }
    }
}
