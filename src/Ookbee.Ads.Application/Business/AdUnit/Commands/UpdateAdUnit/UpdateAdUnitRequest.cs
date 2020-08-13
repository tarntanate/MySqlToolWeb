namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitRequest
    {
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }
        public int? SortSeq { get; set; }
    }
}
