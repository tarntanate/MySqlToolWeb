namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}