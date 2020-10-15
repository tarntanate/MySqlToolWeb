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
        public async Task<Response<List<PlatformReportDto>>> GetAdGroupReportPlatformByAdGroup([FromRoute] int adGroupId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdGroupPlatformReportByGroupIdQuery(adGroupId: adGroupId), cancellationToken);


        [HttpGet("adunit/{adUnitId}")]
        public async Task<Response<List<AdReportByUnitIdDto>>> GetAdImpressionReportByUnitId([FromRoute] int adUnitId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdImpressionReportByUnitIdQuery(unitId: adUnitId), cancellationToken);

        [HttpGet("adunit/{adUnitId}/click")]
        public async Task<Response<List<AdReportByUnitIdDto>>> GetAdClickReportByUnitId([FromRoute] int adUnitId, CancellationToken cancellationToken)
       => await Mediator.Send(new GetAdClickReportByUnitIdQuery(unitId: adUnitId), cancellationToken);

        [HttpGet("ad/{adId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdImpressionPlatformReportByAdId([FromRoute] int adId, CancellationToken cancellationToken)
       => await Mediator.Send(new GetAdImpressionPlatformReportByAdIdQuery(adId: adId), cancellationToken);

        [HttpGet("ad/{adId}")]
        public async Task<Response<List<AdReportByAdIdDto>>> GetAdImpressionReportByAdId([FromRoute] int adId, CancellationToken cancellationToken)
           => await Mediator.Send(new GetAdImpressionReportByAdIdQuery(adId: adId), cancellationToken);

        [HttpGet("ad/{adId}/click")]
        public async Task<Response<List<AdReportByAdIdDto>>> GetAdClickReportByAdId([FromRoute] int adId, CancellationToken cancellationToken)
                  => await Mediator.Send(new GetAdClickReportByAdIdQuery(adId: adId), cancellationToken);

        [HttpGet("campaign/{campaignId}")]
        public async Task<Response<List<AdImpressionReportByCampaignIdDto>>> GetAdImpressionReportByCampaignId([FromRoute] int campaignId, CancellationToken cancellationToken)
          => await Mediator.Send(new GetAdImpressionReportByCampaignIdQuery(campaignId: campaignId), cancellationToken);

        [HttpGet("campaign/{campaignId}/platform")]
        public async Task<Response<List<PlatformReportDto>>> GetAdImpressionPlatformReportByCampaignId([FromRoute] int campaignId, CancellationToken cancellationToken)
        => await Mediator.Send(new GetAdImpressionPlatformReportByCampaignIdQuery(campaignId: campaignId), cancellationToken);

    }
}
