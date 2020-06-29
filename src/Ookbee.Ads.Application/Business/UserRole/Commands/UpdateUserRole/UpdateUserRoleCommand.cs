using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : UpdateUserRoleRequest, IRequest<HttpResult<bool>>
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
