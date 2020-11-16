using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache
{
    public static class AdGroupCacheKey
    {
        public static string GroupIds()
            => $"GROUPS/IDS".ToUpper();
    }
}
