namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitRequest
    {
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public int? SortSeq { get; set; }
    }
}
