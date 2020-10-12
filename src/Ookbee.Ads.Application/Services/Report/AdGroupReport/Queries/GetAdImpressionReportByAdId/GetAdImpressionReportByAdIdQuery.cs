using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByAdId
{
    public class GetAdImpressionReportByAdIdQuery : IRequest<Response<List<AdReportByAdIdDto>>>
    {
        public int AdId { get; set; }

        public GetAdImpressionReportByAdIdQuery(int adId)
        {
            AdId = adId;
        }
    }
}
