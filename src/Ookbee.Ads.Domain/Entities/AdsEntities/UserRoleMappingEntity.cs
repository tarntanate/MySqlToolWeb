using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class UserRoleMappingEntity : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual UserRoleEntity Role { get; set; }
    }
}