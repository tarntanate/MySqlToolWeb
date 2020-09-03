using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId, Platform platform)
            => $"ads/{adId}/{platform}".ToUpper();

        public static string AdStats(long adId)
            => $"ads/{adId}/stats".ToUpper();

        public static string Groups()
            => $"groups".ToUpper();

        public static string GroupStats(long adGroupId, Platform platform)
            => $"groups/{adGroupId}/stats/{platform}".ToUpper();

        public static string Units(long adGroupId, Platform platform)
            => $"groups/{adGroupId}/units/{platform}".ToUpper();

        public static string UnitsAdIds(long adUnitId, Platform platform)
            => $"units/{adUnitId}/ads-ids/{platform}".ToUpper();

        public static string UnitsStats(long adUnitId, Platform platform)
            => $"units/{adUnitId}/stats/{platform}".ToUpper();
    }
}
