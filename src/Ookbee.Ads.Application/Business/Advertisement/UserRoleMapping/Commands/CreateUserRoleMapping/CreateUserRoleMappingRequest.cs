namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}