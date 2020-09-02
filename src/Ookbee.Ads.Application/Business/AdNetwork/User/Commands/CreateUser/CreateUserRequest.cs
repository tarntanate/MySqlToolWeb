namespace Ookbee.Ads.Application.Business.AdNetwork.User.Commands.CreateUser
{
    public class CreateUserRequest
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }
    }
}