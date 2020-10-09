using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByAdId
{
    public class GetAdClickReportByAdIdQuery : IRequest<Response<List<AdImpressionReportByAdIdDto>>>
    {
        public int AdId { get; set; }

        public GetAdClickReportByAdIdQuery(int adId)
        {
            AdId = adId;
        }
    }
}
