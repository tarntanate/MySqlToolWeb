using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId
{
    public class GetAdGroupReportByGroupIdQuery : IRequest<HttpResult<List<AdGroupReportDto>>>
    {
        public int AdGroupId { get; set; }

        public GetAdGroupReportByGroupIdQuery(int adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
