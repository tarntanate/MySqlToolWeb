using Newtonsoft.Json;
using System;

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
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}