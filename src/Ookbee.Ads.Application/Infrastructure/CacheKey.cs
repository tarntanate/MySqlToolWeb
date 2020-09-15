using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ads/{adId}/info".ToUpper();

        public static string AdStats(long adId)
            => $"ads/{adId}/stats".ToUpper();

        public static string Groups()
            => $"groups/info".ToUpper();

        public static string GroupStats(long adGroupId)
            => $"groups/{adGroupId}/stats".ToUpper();

        public static string Units(long adGroupId)
            => $"groups/{adGroupId}/units/info".ToUpper();

        public static string UnitsAdIds(long adUnitId, Platform platform)
            => $"units/{adUnitId}/ads/ids/{platform}".ToUpper();

        public static string UnitsStats(long adUnitId)
            => $"units/{adUnitId}/stats".ToUpper();
    }
}
