using MediatR;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject
{
    public class CopyObjectCommand : IRequest<bool>
    {
        public string SourceAppid { get; set; }
        
        public string SourceBucket { get; set; }

        public string SourceKey { get; set; }

        public string SourceRegion { get; set; }

        public string DestinationBucket { get; set; }

        public string DestinationKey { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, string> QueryParameters { get; set; }
    }
}