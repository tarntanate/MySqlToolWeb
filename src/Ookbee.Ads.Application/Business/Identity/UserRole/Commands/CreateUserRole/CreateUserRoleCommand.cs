using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommand : CreateUserRoleRequest, IRequest<Response<long>>
    {
        public CreateUserRoleCommand(CreateUserRoleRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
