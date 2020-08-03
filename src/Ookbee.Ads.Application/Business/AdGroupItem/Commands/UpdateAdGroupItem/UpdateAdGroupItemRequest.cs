namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem
{
    public class UpdateAdGroupItemRequest
    {
        public long AdUnitId { get; set; }
        public string AdUnitKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortSeq { get; set; }
    }
}
