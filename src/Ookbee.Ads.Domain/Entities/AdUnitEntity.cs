using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities
{
    public class AdUnitEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdUnitTypeEntity AdUnitType { get; set; }
        public virtual PublisherEntity Publisher { get; set; }

        public virtual List<AdEntity> Ads { get; set; }
    }
}
