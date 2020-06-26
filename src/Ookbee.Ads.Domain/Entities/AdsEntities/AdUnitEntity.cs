using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdUnitEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AdNetwork AdNetworks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AdUnitTypeEntity AdUnitType { get; set; }
        public virtual PublisherEntity Publisher { get; set; }

        public virtual List<AdEntity> Ads { get; set; }
    }
}
