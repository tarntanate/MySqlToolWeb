namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache
{
    public static class AdGroupCacheKey
    {
        public static string GroupIdList()
            => $"GROUPS:IDS".ToUpper();
    }
}
