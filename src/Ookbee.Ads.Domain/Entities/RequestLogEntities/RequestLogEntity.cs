using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class RequestLogEntity : BaseEntity, ICreatedAt
    {
        public DateTime? CreatedAt { get; set; }
        public string uuid { get; set; }
        public int? AdId { get; set; }
        public int? AdUnitId { get; set; }
        public short? AdsGroupId { get; set; }
        public short PlatformId { get; set; }
        // public short RequestTypeId { get; set; }
    }
}
