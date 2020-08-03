namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem
{
    public class CreateAdGroupItemRequest
    {
        public long AdGroupId { get; set; }
        public string AdUnitKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortSeq { get; set; }
    }
}
