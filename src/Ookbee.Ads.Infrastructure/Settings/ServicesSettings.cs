namespace Ookbee.Ads.Infrastructure.Settings
{
    public class ServicesSettings
    {
        public AdServicesSettings Ads { get; set; }
        public KafkaRequestSettings Kafka { get; set; }
    }
}
