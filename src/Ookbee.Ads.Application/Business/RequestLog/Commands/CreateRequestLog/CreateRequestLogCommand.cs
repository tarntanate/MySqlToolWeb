using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string AdSlotId { get; set; }

        public string AdId { get; set; }

        public string AppCode { get; set; }

        public string AppVersion { get; set; }

        public string Platform { get; set; }

        public string OsVersion { get; set; }

        public string DeviceId { get; set; }

        public string UserAgents { get; set; }

        public bool EnabledFlag => true;

        public CreateRequestLogCommand()
        {

        }

        public CreateRequestLogCommand(CreateRequestLogCommand request)
        {
            Platform = request.Platform;
        }
    }
}
