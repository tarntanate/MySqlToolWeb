namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupRequest
    {
        public long AdGroupTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Placement { get; set; }
        public bool Enabled { get; set; }
    }
}
