using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByCampaignId
{
    public class GetAdImpressionPlatformReportByCampaignIdQuery : IRequest<Response<List<PlatformReportDto>>>
    {
        public int CampaignId { get; set; }

        public GetAdImpressionPlatformReportByCampaignIdQuery(int campaignId)
        {
            CampaignId = campaignId;
        }
    }
}
