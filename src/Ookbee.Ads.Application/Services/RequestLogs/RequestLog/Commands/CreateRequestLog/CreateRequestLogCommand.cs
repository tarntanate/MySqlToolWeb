using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommand : IRequest<Response<bool>>
    {
        public DateTime CreatedAt { get; set; }
        public short PlatformId { get; set; }
        public short RequestTypeId { get; set; }
        public int? AdId { get; set; }
        public int? AdUnitId { get; set; }
        public short? AdsGroupId { get; set; }
        public string UUID { get; set; }

        public CreateRequestLogCommand(short requestTypeId, short platformId, short? adGroupId, int? adId, int? adUnitId, string uuid)
        {
            CreatedAt = MechineDateTime.Now.DateTime;
            RequestTypeId = requestTypeId;
            PlatformId = platformId;
            AdsGroupId = adGroupId;
            AdUnitId = adUnitId;
            AdId = adId;
            UUID = uuid;
        }
    }
}
