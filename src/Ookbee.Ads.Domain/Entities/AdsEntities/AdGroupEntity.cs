using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdGroupEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdGroupTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }        
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdGroupTypeEntity AdGroupType { get; set; }
        public virtual PublisherEntity Publisher { get; set; }

        public virtual List<AdUnitEntity> AdUnits { get; set; }
        public virtual List<AdGroupStatsEntity> AdGroupStats { get; set; }
    }
}
