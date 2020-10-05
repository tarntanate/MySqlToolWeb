using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog;
using Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis;
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
        public async Task<ContentResult> UpdateAdStats([FromRoute] long adId, [FromQuery] string type, [FromQuery] AdPlatform platform, [FromQuery] int campaignId, [FromQuery] int unitId, CancellationToken cancellationToken)
        {
            if (platform == AdPlatform.Unknown) {
                return new ContentResult() { StatusCode = 500, Content = "Require platform parameter!" };
            }
            var _type = type.ToEnum<AdStatsType>();
            if (_type == AdStatsType.Impression)
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
            if (_type == AdStatsType.Click)
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
           
            var result = await Mediator.Send(new UpdateAdStatsRedisCommand(adId, type.ToEnum<AdStatsType>(), 1), cancellationToken);
            if (result.IsSuccess)
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404, Content = result.Message };
        }
    }
}
