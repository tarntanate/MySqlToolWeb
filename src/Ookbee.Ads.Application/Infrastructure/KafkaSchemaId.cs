using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public abstract class KafkaSchemaId
    {
        public int ValueSchemaId { get; set; }
        public int KeySchemaId { get; set; }
    }

    public class GroupRequestLogSchema : KafkaSchemaId
    {
        public GroupRequestLogSchema()
        {
            ValueSchemaId = GlobalVar.AppSettings.Services.Kafka.GroupRequestLog.ValueSchemaId;
            KeySchemaId = GlobalVar.AppSettings.Services.Kafka.GroupRequestLog.KeySchemaId;
        }
    }
    public class AdImpressionLogSchema : KafkaSchemaId
    {
        public AdImpressionLogSchema()
        {
            ValueSchemaId = GlobalVar.AppSettings.Services.Kafka.AdImpressionLog.ValueSchemaId;
            KeySchemaId = GlobalVar.AppSettings.Services.Kafka.AdImpressionLog.KeySchemaId;
        }
    }

    public class AdClickLogSchema : KafkaSchemaId
    {
        public AdClickLogSchema()
        {
            ValueSchemaId = GlobalVar.AppSettings.Services.Kafka.AdClickLog.ValueSchemaId;
            KeySchemaId = GlobalVar.AppSettings.Services.Kafka.AdClickLog.KeySchemaId;
        }
    }
}
