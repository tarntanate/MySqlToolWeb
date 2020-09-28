using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommand : IRequest<Response<bool>>
    {
        public long UserId { get; private set; }
        public long RoleId { get; private set; }

        public DeleteUserRoleMappingCommand(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
