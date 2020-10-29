using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdGroupStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public long Request { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual AdGroupEntity AdGroup { get; set; }
    }
}
