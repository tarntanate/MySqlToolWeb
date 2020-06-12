using System;

namespace Ookbee.Ads.Application.Business.Analytics
{
    public class RequestLogDto
    {
        public long Id { get; set; }
        public long? AdId { get; set; }
        public long AdUnitId { get; set; }
        public string Platform { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
        public string DeviceId { get; set; }
        public string UserAgent { get; set; }
        public bool IsFill { get; set; }
        public bool IsClick { get; set; }
        public bool IsImpression { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}