namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ads/{adId}";

        public static string Groups()
            => $"groups";

        public static string UnitsByGroup(long adGroupId)
            => $"groups/{adGroupId}/units";

        public static string UnitStatsByGroup(long adGroupId)
            => $"groups/{adGroupId}/stats";

        public static string AdIdsByUnit(long adUnitId, string platform)
            => $"units/{adUnitId}/ads-ids/{platform.ToLower()}";
    }
}
