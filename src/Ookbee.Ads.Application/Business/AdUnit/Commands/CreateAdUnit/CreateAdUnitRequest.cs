namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitRequest
    {
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
