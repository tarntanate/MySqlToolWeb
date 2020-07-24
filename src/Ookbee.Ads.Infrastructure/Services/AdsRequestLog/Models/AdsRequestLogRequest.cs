using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models
{
    public class AdsRequestLogRequest
    {
        public List<AdsRequestLogRecordRequest> Records { get; set; }

        [JsonProperty("key_schema_id")]
        public int KeySchemaId { get; set; }

        [JsonProperty("value_schema_id")]
        public int ValueSchemaId { get; set; }

    }
}
