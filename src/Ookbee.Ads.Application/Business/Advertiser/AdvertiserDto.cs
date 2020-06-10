using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Application.Business.Advertiser
{
    public class AdvertiserDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
