namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string AssetByAd(long adId)
            => $"AD_{adId}_ASSET";

        public static string UnitByGroup(long adGroupId)
            => $"GROUP_{adGroupId}_UNIT";

        public static string AdIdByUnit(string adUnitId)
            => $"UNIT_{adUnitId}_AD";

        public static string AdIdByUnit(string adUnitId, string platform)
            => $"UNIT_{adUnitId}_AD_{platform.ToUpper()}";
    }
}