using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Commands.CreateUser
{
    public class CreateUserCommand : CreateUserRequest, IRequest<HttpResult<long>>
    {
        public CreateUserCommand(CreateUserRequest request)
        {
            Id = request.Id;
            UserName = request.UserName;
            DisplayName = request.DisplayName;
            AvatarUrl = request.AvatarUrl;
        }
    }
}
