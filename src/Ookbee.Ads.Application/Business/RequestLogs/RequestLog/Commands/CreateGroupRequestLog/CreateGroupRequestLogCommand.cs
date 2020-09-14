using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog
{
    public class CreateGroupRequestLogCommand : IRequest<HttpResult<bool>>
    {
        public DateTime CreatedAt { get; set; }
        public short PlatformId { get; set; }
        public short AdGroupId { get; set; }
        public string UUID { get; set; }

        public CreateGroupRequestLogCommand(short adGroupId, short platformId, string uuid)
        {
            CreatedAt = MechineDateTime.Now;
            PlatformId = platformId;
            AdGroupId = adGroupId;
            UUID = uuid;
        }
    }
}
