using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdUnitEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }
        public int? SortSeq { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdGroupEntity AdGroup { get; set; }

        public virtual List<AdEntity> Ads { get; set; }
    }
}
