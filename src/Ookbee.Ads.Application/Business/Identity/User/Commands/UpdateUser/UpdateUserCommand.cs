using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.User.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateUserRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateUserCommand(long id, UpdateUserRequest request)
        {
            Id = id;
            UserName = request.UserName;
            DisplayName = request.DisplayName;
            AvatarUrl = request.AvatarUrl;
        }
    }
}
