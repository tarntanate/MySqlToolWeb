namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}