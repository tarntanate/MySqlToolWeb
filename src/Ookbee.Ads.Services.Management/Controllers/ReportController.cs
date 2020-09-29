using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Report.AdGroupReport;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupPlatformReportByGroupId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        [HttpGet("group/{adGroupId}")]
        public async Task<Response<List<AdSummaryReportDto>>> GetAdGroupReport([FromRoute] int adGroupId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupReportByGroupIdQuery(adGroupId: adGroupId), cancellationToken);
       
        [HttpGet("group/{adGroupId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdGroupReportByPlatform([FromRoute] int adGroupId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupPlatformReportByGroupIdQuery(adGroupId: adGroupId), cancellationToken);

        
        [HttpGet("adunit/{adUnitId}")]
        public async Task<Response<List<AdUnitImpressionReportDto>>> GetAdImpressionReport([FromRoute] int adUnitId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdImpressionReportByUnitIdQuery(adUnitId), cancellationToken);

    }
}
