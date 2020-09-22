using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.User.Commands.CreateUser
{
    public class CreateUserCommand : CreateUserRequest, IRequest<Response<long>>
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
