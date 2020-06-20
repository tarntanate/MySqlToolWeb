namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
