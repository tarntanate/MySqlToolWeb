namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ads/{adId}".ToLower();

        public static string Groups()
            => $"groups".ToLower();

        public static string UnitsByGroup(long adGroupId)
            => $"groups/{adGroupId}/units".ToLower();

        public static string UnitStatsByGroup(long adGroupId)
            => $"groups/{adGroupId}/stats".ToLower();

        public static string AdIdsByUnit(long adUnitId, string platform)
            => $"units/{adUnitId}/ads-ids/{platform}".ToLower();
    }
}
