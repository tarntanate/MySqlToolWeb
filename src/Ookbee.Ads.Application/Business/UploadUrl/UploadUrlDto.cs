namespace Ookbee.Ads.Application.Business.SlotType
{
    public class UploadUrlDto
    {
        public string Id { get; set; }
        
        public string AppId { get; set; }
        
        public string Region { get; set; }

        public string SourceBucket { get; set; }

        public string DestinationBucket { get; set; }

        public string Key { get; set; }

        public string SignUrl { get; set; }
    }
}