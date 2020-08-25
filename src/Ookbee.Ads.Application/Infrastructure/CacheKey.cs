using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ads/{adId}".ToLower();

        public static string AdStats(long adId)
            => $"ads/{adId}/stats".ToLower();

        public static string Groups()
            => $"groups".ToLower();

        public static string GroupStats(long adGroupId)
            => $"groups/{adGroupId}/stats".ToLower();

        public static string Units(long adGroupId)
            => $"groups/{adGroupId}/units".ToLower();

        public static string UnitsAdIds(long adUnitId, Platform platform)
            => $"units/{adUnitId}/ads/{platform}".ToLower();

        public static string UnitsStats(long adUnitId)
            => $"units/{adUnitId}/stats".ToLower();
    }
}
