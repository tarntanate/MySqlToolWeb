using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string AdIds()
            => $"ads/ids".ToUpper();

        public static string AdPlatforms(long adId)
            => $"ads/{adId}/platforms".ToUpper();

        public static string GroupIds()
            => $"groups/ids".ToUpper();

        public static string GroupStats(long adGroupId)
            => $"groups/{adGroupId}/stats".ToUpper();

        public static string GroupUnitIds(long adGroupId)
            => $"groups/{adGroupId}/units/ids".ToUpper();

        public static string GroupUnitPlatforms(long adGroupId)
            => $"groups/{adGroupId}/units/platforms".ToUpper();

        public static string UnitIds()
            => $"units/ids".ToUpper();

        public static string UnitAdIds(long adUnitId)
            => $"units/{adUnitId}/ads/ids".ToUpper();

        public static string UnitAdIdsPreview(long adUnitId)
            => $"units/{adUnitId}/ads/ids/preview".ToUpper();

        public static string UnitAdIds(long adUnitId, AdPlatform platform)
            => $"units/{adUnitId}/ads/ids-{platform}".ToUpper();

        public static string UnitAdIdsPreview(long adUnitId, AdPlatform platform)
            => $"units/{adUnitId}/ads/ids-{platform}/preview".ToUpper();

        public static string UnitStats(long adUnitId)
            => $"units/{adUnitId}/stats".ToUpper();
















        public static string AdStats(long adId)
            => $"ads/{adId}/stats".ToUpper();

        public static string Groups()
            => $"groups/info".ToUpper();

        public static string UnitsAdIdsPreview(long adUnitId, AdPlatform platform)
            => $"units/{adUnitId}/ads/{platform}/preview".ToUpper();

        public static string UserPreview()
            => $"users/preview".ToUpper();
    }
}
