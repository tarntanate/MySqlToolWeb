namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionRequest
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string ExtensionName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
