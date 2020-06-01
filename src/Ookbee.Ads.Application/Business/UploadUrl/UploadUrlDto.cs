using System;
using System.Text.Json.Serialization;

namespace Ookbee.Ads.Application.Business.UploadUrl
{
    public class UploadUrlDto
    {
        public string Id { get; set; }

        public string MapperId { get; set; }

        public string MapperType { get; set; }

        public string MimeType { get; set; }

        public string FileExtension { get; set; }

        public string AppId { get; set; }

        public string Region { get; set; }

        public string SourceBucket { get; set; }

        public string SourceKey { get; set; }

        public string DestinationBucket { get; set; }

        public string DestinationKey { get; set; }

        public string SignedUrl { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public bool EnabledFlag { get; set; }
    }
}