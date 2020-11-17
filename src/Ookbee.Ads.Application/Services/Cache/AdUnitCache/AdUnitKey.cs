namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache
{
    public static class AdUnitKey
    {
        public static string Ids()
            => $"UNITS-IDS".ToUpper();

        public static string Ids(long adGroupId)
            => $"GROUPS:{adGroupId}:{Ids()}".ToUpper();
    }
}
