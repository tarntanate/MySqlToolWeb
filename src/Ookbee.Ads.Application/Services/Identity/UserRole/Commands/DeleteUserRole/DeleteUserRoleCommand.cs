using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteUserRoleCommand(long id)
        {
            Id = id;
        }
    }
}
