using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteUserRoleCommand(long id)
        {
            Id = id;
        }
    }
}
