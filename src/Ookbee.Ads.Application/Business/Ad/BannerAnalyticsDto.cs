using System.Collections.Generic;
using Newtonsoft.Json;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class BannerAnalyticsDto
    {
        private long AdId { get; }
        private long AdUnitId { get; }
        private string BaseUri => GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
        [JsonIgnore]
        private List<string> clicks;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Clicks
        {
            get
            {
                if (!clicks.HasValue())
                    clicks = new List<string>();

                clicks.Insert(0, $"{BaseUri}/api/statistics?adUnitId={AdUnitId}&adsId={AdId}&event=click");

                return clicks;
            }
            set
            {
                clicks = value;
            }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonIgnore]
        private List<string> impressions;

        public List<string> Impressions
        {
            get
            {
                if (!impressions.HasValue())
                    impressions = new List<string>();

                impressions.Insert(0, $"{BaseUri}/api/statistics?adUnitId={AdUnitId}&adsId={AdId}&event=impression");

                return impressions;
            }
            set
            {
                impressions = value;
            }
        }

        public BannerAnalyticsDto(long adId, long adUnitId)
        {
            AdId = adId;
            AdUnitId = adUnitId;
        }
    }
}
