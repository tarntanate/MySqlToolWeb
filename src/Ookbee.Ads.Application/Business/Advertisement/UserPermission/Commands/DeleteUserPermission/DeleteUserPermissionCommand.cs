using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteUserPermissionCommand(long id)
        {
            Id = id;
        }
    }
}
