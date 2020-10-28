using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public long Quota { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
