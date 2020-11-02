using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Infrastructure
{
    public abstract class KafkaSchemaId
    {
        public int value_schema_id { get; set; }
        public int key_schema_id { get; set; }
    }

    public class GroupRequestLogSchema : KafkaSchemaId
    {
        public GroupRequestLogSchema()
        {
            value_schema_id = 46;
            key_schema_id = 47;
        }
    }
    public class AdImpressionLogSchema : KafkaSchemaId
    {
        public AdImpressionLogSchema()
        {
            value_schema_id = 48;
            key_schema_id = 49;
        }
    }

     public class AdClickLogSchema : KafkaSchemaId
    {
        public AdClickLogSchema()
        {
            value_schema_id = 50;
            key_schema_id = 51;
        }
    }
}
