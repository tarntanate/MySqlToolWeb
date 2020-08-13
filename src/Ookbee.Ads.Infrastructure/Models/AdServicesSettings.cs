namespace Ookbee.Ads.Infrastructure.Models
{
    public class AdServicesSettings
    {
        public BaseUriSettings<AddressSettings> Analytics { get; set; }
        public BaseUriSettings<AddressSettings> Identity { get; set; }
        public BaseUriSettings<AddressSettings> Management { get; set; }
        public BaseUriSettings<AddressSettings> Publish { get; set; }
        public BaseUriSettings<AddressSettings> RequestLog { get; set; }
    }
}
