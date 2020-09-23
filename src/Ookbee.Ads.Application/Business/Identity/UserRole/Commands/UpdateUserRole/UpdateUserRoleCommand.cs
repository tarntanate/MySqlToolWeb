using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : UpdateUserRoleRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateUserRoleCommand(long id, UpdateUserRoleRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
