using System;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommand : IRequest<HttpResult<string>>
    {
        private string Id => ObjectId.GenerateNewId().ToString();

        private string AdId => null;

        public string AdSlotId { get; set; }

        public string DeviceId { get; set; }

        public string Platform { get; set; }

        public string OsVersion { get; set; }

        public string AppCode { get; set; }

        public string AppVersion { get; set; }

        public string UserAgents { get; set; }

        public DateTime? CreatedAt => MechineDateTime.Now.DateTime;

        public CreateRequestLogCommand()
        {

        }

        public CreateRequestLogCommand(string adSlotId, string deviceId, string platform, string userAgent)
        {
            AdSlotId = adSlotId;
            DeviceId = deviceId;
            Platform = platform;
            UserAgents = userAgent;
        }
    }
}
