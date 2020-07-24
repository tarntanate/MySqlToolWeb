using System;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdsRequestLogValueRequest
    {
        public int AdId { get; set; }
        public int AdUnitId { get; set; }
        public int Platform { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
        public string DeviceId { get; set; }
        public string UserAgent { get; set; }
        public bool IsFill { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsClick { get; set; }
        public bool IsImpression { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UUID { get; set; }
    }
}