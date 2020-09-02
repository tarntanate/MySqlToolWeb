namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingRequest
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}