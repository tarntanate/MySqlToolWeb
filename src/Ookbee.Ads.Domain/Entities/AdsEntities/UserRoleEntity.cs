using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class UserRoleEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual List<UserPermissionEntity> Permissions { get; set; }
        public virtual List<UserRoleMappingEntity> UserRoleMappings { get; set; }
    }
}