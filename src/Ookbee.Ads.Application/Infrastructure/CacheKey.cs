using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ads/{adId}".ToUpper();

        public static string AdStats(long adId)
            => $"ads/{adId}/stats".ToUpper();

        public static string Groups()
            => $"groups".ToUpper();

        public static string GroupStats(long adGroupId, Platform platform)
            => $"groups/{adGroupId}/stats/{platform}".ToUpper();

        public static string Units(long adGroupId)
            => $"groups/{adGroupId}/units".ToUpper();

        public static string UnitsAdIds(long adUnitId, Platform platform)
            => $"units/{adUnitId}/ads/{platform}".ToUpper();

        public static string UnitsStats(long adUnitId)
            => $"units/{adUnitId}/stats".ToUpper();
    }
}
