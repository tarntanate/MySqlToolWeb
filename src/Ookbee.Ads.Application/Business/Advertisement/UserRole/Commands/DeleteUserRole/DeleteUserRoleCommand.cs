using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteUserRoleCommand(long id)
        {
            Id = id;
        }
    }
}
