namespace Ookbee.Ads.Application.Services.Identity.User.Commands.UpdateUser
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
