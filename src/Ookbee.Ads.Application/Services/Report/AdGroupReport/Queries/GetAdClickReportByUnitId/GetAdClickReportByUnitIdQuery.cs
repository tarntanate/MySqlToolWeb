using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByUnitId
{
    public class GetAdClickReportByUnitIdQuery : IRequest<Response<List<AdReportByUnitIdDto>>>
    {
        public int UnitId { get; set; }

        public GetAdClickReportByUnitIdQuery(int unitId)
        {
            UnitId = unitId;
        }
    }
}
