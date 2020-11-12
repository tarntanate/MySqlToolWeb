using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByAdId
{
    public class GetAdClickReportByAdIdQuery : IRequest<Response<List<AdReportByAdIdDto>>>
    {
        public int AdId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAdClickReportByAdIdQuery(int adId, DateTime startDate, DateTime endDate)
        {
            AdId = adId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
