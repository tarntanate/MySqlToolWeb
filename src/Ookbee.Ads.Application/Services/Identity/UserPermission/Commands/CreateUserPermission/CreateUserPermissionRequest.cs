namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionRequest
    {
        public long RoleId { get; set; }
        public string ExtensionName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}