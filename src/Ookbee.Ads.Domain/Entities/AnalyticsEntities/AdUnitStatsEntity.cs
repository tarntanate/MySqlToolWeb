using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class AdUnitStatsEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }
        public DateTime CaculatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
