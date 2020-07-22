namespace Ookbee.Ads.Infrastructure.Models
{
    public class AdServicesSettings
    {
        public BaseUriSettings<AddressSettings> Analytics { get; set; }
        public BaseUriSettings<AddressSettings> Banner { get; set; }
        public BaseUriSettings<AddressSettings> Identity { get; set; }
        public BaseUriSettings<AddressSettings> Manager { get; set; }
        public BaseUriSettings<AddressSettings> RequestLog { get; set; }
    }
}
