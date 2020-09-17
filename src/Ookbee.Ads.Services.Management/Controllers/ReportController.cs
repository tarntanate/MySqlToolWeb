using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Report.AdGroupReport;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        [HttpGet("group/{id}")]
        public async Task<HttpResult<List<AdGroupReportDto>>> GetList([FromRoute] int id, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupReportByGroupIdQuery(adGroupId: id), cancellationToken);

    }
}
