namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitRequest
    {
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public int? SortSeq { get; set; }
    }
}
