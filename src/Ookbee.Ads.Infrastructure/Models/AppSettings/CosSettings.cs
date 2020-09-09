namespace Ookbee.Ads.Infrastructure.Models
{
    public class CosSettings
    {
        public string AppId { get; set; }
        public string Region { get; set; }
        public string SecretId { get; set; }
        public string SecretKey { get; set; }
        public long DurationSecond { get; set; }
        public int ConnectionTimeoutMs { get; set; }
        public int ReadWriteTimeoutMs { get; set; }
        public bool DebugLog { get; set; }
        public bool IsHttps { get; set; }
        public BucketSettings Bucket { get; set; }
        public CosBaseUriSettings BaseUri { get; set; }
    }
}