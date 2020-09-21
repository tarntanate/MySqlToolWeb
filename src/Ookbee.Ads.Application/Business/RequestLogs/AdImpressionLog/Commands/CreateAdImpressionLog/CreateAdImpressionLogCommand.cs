using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog
{
    public class CreateAdImpressionLogCommand : IRequest<HttpResult<bool>>
    {
        public DateTime CreatedAt { get; set; }
        public short PlatformId { get; set; }
        public int AdId { get; set; }
        public int CampaignId { get; set; }
        public string UUID { get; set; }

        public CreateAdImpressionLogCommand(int adId, int campaignId, short platformId, string uuid)
        {
            CreatedAt = MechineDateTime.Now.DateTime;
            AdId = adId;
            CampaignId = campaignId;
            PlatformId = platformId;
            UUID = uuid;
        }
    }
}
