using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher
{
    public class PublisherDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string CountryCode { get; set; }
    }
}
