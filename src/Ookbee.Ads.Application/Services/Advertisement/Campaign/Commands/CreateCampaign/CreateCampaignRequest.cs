namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignRequest
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
