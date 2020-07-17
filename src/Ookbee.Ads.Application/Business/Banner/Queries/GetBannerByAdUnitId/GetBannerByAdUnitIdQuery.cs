using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId
{
    public class GetBannerByAdUnitIdQuery : IRequest<HttpResult<BannerDto>>
    {
        public long AdUnitId { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
        public string DeviceId { get; set; }
        public string DeviceOs { get; set; }
        public string Platform { get; set; }
        public string UserAgent { get; set; }

        public GetBannerByAdUnitIdQuery(long adUnitId, string appCode, string appVersion, string deviceId, string deviceOs, string platform, string userAgent)
        {
            AdUnitId = adUnitId;
            AppCode = appCode;
            AppVersion = appVersion;
            DeviceId = deviceId;
            DeviceOs = deviceOs;
            Platform = platform;
            UserAgent = userAgent;
        }
    }
}
