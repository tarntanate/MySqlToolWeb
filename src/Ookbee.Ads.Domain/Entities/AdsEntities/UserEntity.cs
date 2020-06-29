using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class UserEntity : BaseEntity, ICreatedAt, IUpdatedAt
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string AvartarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual List<UserRoleMappingEntity> UserRoleMappings { get; set; }
    }
}