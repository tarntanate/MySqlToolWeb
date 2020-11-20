using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis
{
    public class AdAssetCacheDto
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public AdPosition Position { get; set; }
    }
}
