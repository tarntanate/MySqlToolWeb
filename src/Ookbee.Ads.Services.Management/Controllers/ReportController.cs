using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Report.AdGroupReport;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByAdId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByUnitId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupPlatformReportByGroupId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByAdId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByCampaignId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByAdId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByCampaignId;
using Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        [HttpGet("group/{adGroupId}")]
        public async Task<Response<List<AdSummaryReportDto>>> GetAdGroupReport([FromRoute] int adGroupId,[FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupReportByGroupIdQuery(adGroupId: adGroupId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("group/{adGroupId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdGroupReportPlatformByAdGroup([FromRoute] int adGroupId, [FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupPlatformReportByGroupIdQuery(adGroupId: adGroupId, startDate: start, endDate: end), cancellationToken);


        [HttpGet("adunit/{adUnitId}")]
        public async Task<Response<List<AdReportByUnitIdDto>>> GetAdImpressionReportByUnitId([FromRoute] int adUnitId,[FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdImpressionReportByUnitIdQuery(unitId: adUnitId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("adunit/{adUnitId}/click")]
        public async Task<Response<List<AdReportByUnitIdDto>>> GetAdClickReportByUnitId([FromRoute] int adUnitId,[FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
       => await Mediator.Send(new GetAdClickReportByUnitIdQuery(unitId: adUnitId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("ad/{adId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdImpressionPlatformReportByAdId([FromRoute] int adId, [FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
       => await Mediator.Send(new GetAdImpressionPlatformReportByAdIdQuery(adId: adId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("ad/{adId}")]
        public async Task<Response<List<AdReportByAdIdDto>>> GetAdImpressionReportByAdId([FromRoute] int adId, [FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdImpressionReportByAdIdQuery(adId: adId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("ad/{adId}/click")]
        public async Task<Response<List<AdReportByAdIdDto>>> GetAdClickReportByAdId([FromRoute] int adId, [FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
                  => await Mediator.Send(new GetAdClickReportByAdIdQuery(adId: adId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("campaign/{campaignId}")]
        public async Task<Response<List<AdImpressionReportByCampaignIdDto>>> GetAdImpressionReportByCampaignId([FromRoute] int campaignId, [FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
          => await Mediator.Send(new GetAdImpressionReportByCampaignIdQuery(campaignId: campaignId, startDate: start, endDate: end), cancellationToken);

        [HttpGet("campaign/{campaignId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdImpressionPlatformReportByCampaignId([FromRoute] int campaignId,[FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken cancellationToken)
        => await Mediator.Send(new GetAdImpressionPlatformReportByCampaignIdQuery(campaignId: campaignId, startDate: start, endDate: end), cancellationToken);

    }
}
