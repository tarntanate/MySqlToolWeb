using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
using Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog;
using Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adId}/stats")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<ContentResult> UpdateAdStats([FromRoute] long adId, [FromQuery] string type, [FromQuery] Platform platform, [FromQuery] int campaignId, [FromQuery] int unitId, CancellationToken cancellationToken)
        {
            if (type.ToLower() == "impression")
            {
                var timescaleResult = await Mediator.Send(
                    new CreateAdImpressionLogCommand(
                        platformId: (short)platform,
                        adId: (int)adId,
                        unitId: unitId,
                        campaignId: campaignId,
                        uuid: new Random().Next(0, 20).ToString()),
                        cancellationToken);
            }
             if (type.ToLower() == "click")
            {
                var timescaleResult = await Mediator.Send(
                    new CreateAdClickLogCommand(
                        platformId: (short)platform,
                        adId: (int)adId,
                        unitId: unitId,
                        campaignId: campaignId,
                        uuid: new Random().Next(0, 20).ToString()),
                        cancellationToken);
            }

            var result = await Mediator.Send(new IncrementAdStatsCacheCommand(type.ToEnum<StatsType>(), adId), cancellationToken);
            if (result.Ok &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
