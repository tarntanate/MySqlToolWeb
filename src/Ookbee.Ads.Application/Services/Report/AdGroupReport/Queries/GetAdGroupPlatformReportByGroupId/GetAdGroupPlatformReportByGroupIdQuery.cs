using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupPlatformReportByGroupId
{
    public class GetAdGroupPlatformReportByGroupIdQuery : IRequest<Response<List<PlatformReportDto>>>
    {
        public int AdGroupId { get; set; }
         public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdGroupPlatformReportByGroupIdQuery(int adGroupId, DateTime startDate, DateTime endDate)
        {
            AdGroupId = adGroupId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
