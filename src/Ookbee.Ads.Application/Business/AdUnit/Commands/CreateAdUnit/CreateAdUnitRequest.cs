namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitRequest
    {
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }
        public int? SortSeq { get; set; }
    }
}
