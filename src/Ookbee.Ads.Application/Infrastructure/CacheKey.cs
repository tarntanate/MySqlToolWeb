namespace Ookbee.Ads.Application.Infrastructure
{
    public static class CacheKey
    {
        public static string GroupItemList(long adGroupId) => $"AD_GROUP_{adGroupId}_ITEMS";
    }
}