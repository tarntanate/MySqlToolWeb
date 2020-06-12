using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdUnitTypeEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual List<AdUnitEntity> AdUnits { get; set; }
    }
}
