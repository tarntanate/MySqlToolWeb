using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class UserPermissionEntity : BaseEntity, IBaseIdentity
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string ExtensionName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        public virtual UserRoleEntity Role { get; set; }
    }
}