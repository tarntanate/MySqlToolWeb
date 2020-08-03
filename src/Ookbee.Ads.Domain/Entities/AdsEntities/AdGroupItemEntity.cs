using System;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdGroupItemEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public string AdUnitKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortSeq { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AdGroupEntity AdGroup { get; set; }
    }
}
