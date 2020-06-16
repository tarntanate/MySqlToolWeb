using System;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AnalyticsEntities
{
    public class RequestLogEntity : BaseEntity, ICreatedAt
    {
        public long? AdId { get; set; }
        public long AdUnitId { get; set; }
        public string Platform { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
        public string DeviceId { get; set; }
        public string UserAgent { get; set; }
        public bool IsFill { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsClick { get; set; }
        public bool IsImpression { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
