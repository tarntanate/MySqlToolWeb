namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}