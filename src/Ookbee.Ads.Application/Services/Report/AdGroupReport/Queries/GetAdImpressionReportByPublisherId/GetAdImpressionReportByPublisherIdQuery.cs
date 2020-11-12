using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByPublisherId
{
    public class GetAdImpressionReportByPublisherIdQuery : IRequest<Response<List<AdImpressionReportDto>>>
    {
        public int PublisherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdImpressionReportByPublisherIdQuery(int publisherId, DateTime startDate, DateTime endDate)
        {
            PublisherId = publisherId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
