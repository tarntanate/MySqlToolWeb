using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string AvatarUrl { get; private set; }
        public long RoleId { get; private set; }

        public UpdateUserCommand(long id, UpdateUserRequest request)
        {
            Id = id;
            UserName = request.UserName;
            DisplayName = request.DisplayName;
            AvatarUrl = request.AvatarUrl;
            RoleId = request.RoleId;
        }
    }
}
