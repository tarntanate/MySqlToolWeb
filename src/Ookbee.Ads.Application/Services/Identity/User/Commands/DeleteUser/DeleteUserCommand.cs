using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteUserCommand(long id)
        {
            Id = id;
        }
    }
}
