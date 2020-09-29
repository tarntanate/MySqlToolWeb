using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId
{
    public class GetAdImpressionReportByUnitIdQuery : IRequest<Response<List<AdUnitImpressionReportDto>>>
    {
        public int AdUnitId { get; set; }

        public GetAdImpressionReportByUnitIdQuery(int unitId)
        {
            AdUnitId = unitId;
        }
    }
}
