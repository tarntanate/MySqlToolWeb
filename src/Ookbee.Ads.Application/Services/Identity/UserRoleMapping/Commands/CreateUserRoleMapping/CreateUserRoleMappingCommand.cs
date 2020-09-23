using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommand : IRequest<Response<bool>>
    {
        public long UserId { get; private set; }
        public long RoleId { get; private set; }

        public CreateUserRoleMappingCommand(CreateUserRoleMappingRequest request)
        {
            UserId = request.UserId;
            RoleId = request.RoleId;
        }
    }
}
