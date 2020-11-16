using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByCampaignId
{
    public class GetAdImpressionReportByCampaignIdQuery : IRequest<Response<List<AdImpressionReportByCampaignIdDto>>>
    {
        public int CampaignId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdImpressionReportByCampaignIdQuery(int campaignId, DateTime startDate, DateTime endDate)
        {
            CampaignId = campaignId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
