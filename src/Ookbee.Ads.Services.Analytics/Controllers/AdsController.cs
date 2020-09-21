using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
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
        public async Task<Unit> UpdateAdStats([FromRoute] long adId, [FromQuery] string type, [FromQuery] string platform, CancellationToken cancellationToken)
        {
            
             // For Testing TimeScaleDb
            short platformId = 1; // (short) platform
            if (type.ToLower() == "impression") {

                var timescaleResult = await Mediator.Send(
                    new CreateAdImpressionLogCommand(
                        platformId: platformId,
                        adId: (int) adId,
                        campaignId: 1,
                        uuid: new Random().Next(0,20).ToString()),
                        cancellationToken);
            }

            var result = await Mediator.Send(new IncrementAdStatsCacheCommand(type.ToEnum<StatsType>(), adId), cancellationToken);
            return result;
        }
            
    }
}
