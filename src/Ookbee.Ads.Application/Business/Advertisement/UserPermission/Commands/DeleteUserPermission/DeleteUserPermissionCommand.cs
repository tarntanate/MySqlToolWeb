using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteUserPermissionCommand(long id)
        {
            Id = id;
        }
    }
}
