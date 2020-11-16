using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis
{
    public class AdAssetCacheDto
    {
        public string AssetType { get; set; }
        public string AssetUrl { get; set; }
        public AdPosition Position { get; set; }
    }
}
