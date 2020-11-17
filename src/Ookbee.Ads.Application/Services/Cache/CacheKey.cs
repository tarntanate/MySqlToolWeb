namespace Ookbee.Ads.Application.Services.Cache
{
    public static class CacheKey
    {
        public static string GroupIdList()
            => $"GROUP:ID".ToUpper();

        public static string UnitIdList()
            => $"UNIT:ID".ToUpper();

        public static string UnitIdListByGroupId(long adGroupId)
            => $"GROUP:{adGroupId}:UNIT-ID".ToUpper();

        public static string UnitListByPlatform(long adGroupId)
            => $"GROUP:{adGroupId}:UNIT-PLATFORM".ToUpper();
    }
}
