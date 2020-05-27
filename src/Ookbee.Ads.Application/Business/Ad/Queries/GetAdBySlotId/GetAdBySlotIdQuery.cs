using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdBySlotId
{
    public class GetAdBySlotIdQuery : IRequest<HttpResult<AdItemDto>>
    {
        public string AdSlotId { get; set; }

        public string Platform { get; set; }

        public string DeviceId { get; set; }

        public string DeviceOsVersion { get; set; }

        public string AppCode { get; set; }

        public string AppVersion { get; set; }

        public string UserAgent { get; set; }

        public GetAdBySlotIdQuery(string appCode, string appVersion, string adSlotId, string deviceId, string platform, string userAgent)
        {
            AppCode = appCode;
            AppVersion = appVersion;
            AdSlotId = adSlotId;
            DeviceId = deviceId;
            Platform = platform;
            UserAgent = userAgent;
        }
    }
}
