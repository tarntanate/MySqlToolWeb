using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByAdId
{
    public class GetAdImpressionPlatformReportByAdIdQuery : IRequest<Response<List<PlatformReportDto>>>
    {
        public int AdId { get; set; }

        public GetAdImpressionPlatformReportByAdIdQuery(int adId)
        {
            AdId = adId;
        }
    }
}
