using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommand : IRequest<Response<bool>>
    {
        public long UserId { get; private set; }
        public long RoleId { get; private set; }

        public UpdateUserRoleMappingCommand(UpdateUserRoleMappingRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
