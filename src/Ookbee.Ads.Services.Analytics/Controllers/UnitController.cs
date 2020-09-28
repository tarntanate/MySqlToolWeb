using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog;
using Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog;
using Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adUnitId}/stats")]
    public class UnitsController : ApiController
    {
        [HttpGet]
        public async Task<ContentResult> UpdateUnitStats([FromRoute] long adUnitId, [FromQuery] AdPlatform platform, [FromQuery] string type, CancellationToken cancellationToken)
        {
            var _type = type.ToEnum<AdStatsType>();
            if (_type == AdStatsType.Impression)
            {
                var timescaleResult = await Mediator.Send(
                        new CreateAdImpressionLogCommand(
                            platformId: (short)platform,
                            adId: 0,
                            unitId: (int)adUnitId,
                            campaignId: 0,
                            uuid: new Random().Next(0, 20).ToString()),
                            cancellationToken);
            }
            if (_type == AdStatsType.Click)
            {
                var timescaleResult = await Mediator.Send(
                        new CreateAdClickLogCommand(
                            platformId: (short)platform,
                            adId: 0,
                            unitId: (int)adUnitId,
                            campaignId: 0,
                            uuid: new Random().Next(0, 20).ToString()),
                            cancellationToken);
            }
         
            var result = await Mediator.Send(new IncrementAdUnitStatsCacheCommand(type.ToEnum<AdStatsType>(), adUnitId), cancellationToken);
            if (result.IsSuccess &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
