namespace Ookbee.Ads.Infrastructure.Settings
{
    public class KafkaRequestSettings
    {
        public KafkaSchemaSettings GroupRequestLog { get; set; }
        public KafkaSchemaSettings AdImpressionLog { get; set; }
        public KafkaSchemaSettings AdClickLog { get; set; }
    }
}
