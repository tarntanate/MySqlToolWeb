using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdPlatformDto : DefaultDto
    {
        public IEnumerable<AdPlatform> Platforms { get; set; }
        public AdStatusType Status { get; set; }
    }
}