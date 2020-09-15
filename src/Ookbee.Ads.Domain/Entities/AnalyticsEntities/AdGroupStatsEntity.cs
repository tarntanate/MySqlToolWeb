using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class AdGroupStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public long Request { get; set; }
        public DateTime CaculatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
