using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdUnitStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
