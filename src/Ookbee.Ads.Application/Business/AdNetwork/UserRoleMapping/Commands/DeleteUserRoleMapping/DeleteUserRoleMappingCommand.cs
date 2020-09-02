using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommand : IRequest<HttpResult<bool>>
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public DeleteUserRoleMappingCommand(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
