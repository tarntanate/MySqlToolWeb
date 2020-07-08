using System.Collections.Generic;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdRequest
    {
        public long AdUnitId { get; set; }
        public long CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public int? CooldownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public List<string> Analytics { get; set; }
        public List<Platform> Platforms { get; set; }
        public string AppLink { get; set; }
        public string LinkUrl { get; set; }
    }
}
