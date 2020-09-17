using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class AdGroupStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public long Request { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
