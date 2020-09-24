using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos
{
    public class GenerateSignURLCommand : IRequest<Response<string>>
    {
        public string Bucket { get; set; }

        public string Key { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, string> QueryParameters { get; set; }
    }
}
