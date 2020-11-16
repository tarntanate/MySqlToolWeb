using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId
{
    public class GetAdImpressionReportByUnitIdQuery : IRequest<Response<List<AdReportByUnitIdDto>>>
    {
        public int AdUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdImpressionReportByUnitIdQuery(int unitId, DateTime startDate, DateTime endDate)
        {
            AdUnitId = unitId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
