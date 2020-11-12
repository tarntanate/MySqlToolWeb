using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public UpdateUserRoleCommand(long id, UpdateUserRoleRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
