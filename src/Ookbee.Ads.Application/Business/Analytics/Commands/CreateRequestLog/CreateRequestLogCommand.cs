using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.CreateRequestLog
{
    public class CreateRequestLogCommand : IRequest<HttpResult<long>>
    {
        public long? AdId { get; set; }
        public long AdUnitId { get; set; }
        public string Platform { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
        public string DeviceId { get; set; }
        public string UserAgent { get; set; }
        public bool IsFill { get; set; }
        public bool IsClick { get; set; }
        public bool IsImpression { get; set; }

        public CreateRequestLogCommand()
        {

        }

        public CreateRequestLogCommand(CreateRequestLogCommand request)
        {
            AdId = request.AdId;
            AdUnitId = request.AdUnitId;
            Platform = request.Platform;
            AppCode = request.AppCode;
            AppVersion = request.AppVersion;
            DeviceId = request.DeviceId;
            UserAgent = request.UserAgent;
        }
    }
}
