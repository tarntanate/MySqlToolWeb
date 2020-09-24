using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommand : IRequest<Response<long>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CreateUserRoleCommand(CreateUserRoleRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
