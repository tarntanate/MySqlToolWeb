using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class AdStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public long Quota { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }
        public DateTime CaculatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
