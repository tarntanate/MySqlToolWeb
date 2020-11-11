using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser
{
    public class AdvertiserDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalCampaign { get; set; }
    }
}
