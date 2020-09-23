namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}