using MediatR;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos
{
    public class GenerateSignURLCommand : IRequest<string>
    {
        public string MapperId { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}
