using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId
{
    public class GetAdGroupReportByGroupIdQuery : IRequest<Response<List<AdSummaryReportDto>>>
    {
        public int AdGroupId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdGroupReportByGroupIdQuery(int adGroupId, DateTime startDate, DateTime endDate)
        {
            AdGroupId = adGroupId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
