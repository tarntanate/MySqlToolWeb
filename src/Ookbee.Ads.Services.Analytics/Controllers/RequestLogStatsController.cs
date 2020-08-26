using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.Commands.AdAssetsStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/units/{unitId}/ads/{adId}/stats")]
    public class RequestLogStatsController : ApiController
    {
        [HttpGet]
        public async Task Get(
            [FromRoute] long unitId,
            [FromRoute] long adId,
            [FromQuery(Name = "event")] string eventType,
            CancellationToken cancellationToken)
        {
            if (Enum.TryParse(eventType, true, out AdStats stats))
            {
                await Mediator.Send(new UpdateAdAssetsStatsCacheCommand(unitId, adId, stats), cancellationToken);
            }
        }
    }
}
