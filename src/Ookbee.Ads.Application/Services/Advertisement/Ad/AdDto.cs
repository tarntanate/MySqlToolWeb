using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.Campaign;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatusType Status { get; set; }
        public int? Quota { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string LinkUrl { get; set; }
        public IEnumerable<string> Analytics { get; set; }
        public IEnumerable<AdPlatform> Platforms { get; set; }
        public IEnumerable<AdAssetDto> Assets { get; set; }
        public AdUnitDto AdUnit { get; set; }
        public CampaignDto Campaign { get; set; }
    }
}