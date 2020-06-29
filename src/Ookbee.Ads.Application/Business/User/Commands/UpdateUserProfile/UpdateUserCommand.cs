using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.User.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateUserRequest, IRequest<HttpResult<bool>>
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
