namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string Ad(long adId)
            => $"ADS_{adId}";

        public static string UnitsByGroup(long adGroupId)
            => $"UNITS_GROUPS_{adGroupId}";

        public static string UnitStatsByGroup(long adGroupId)
            => $"STATS_GROUPS_{adGroupId}";

        public static string AdIdsByUnit(long adUnitId, string platform)
            => $"ADS_{platform.Substring(0, 3).ToUpper()}_UNITS_{adUnitId}";
    }
}
