namespace Ookbee.Ads.Application.Business.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionRequest
    {
        public long RoleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}