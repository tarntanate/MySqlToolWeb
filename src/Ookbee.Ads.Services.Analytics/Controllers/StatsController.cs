﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdAssetsStatsCache.Commands.IncrementAdAssetsStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/units/{unitId}/ads/{adId}/stats")]
    public class StatsController : ApiController
    {
        [HttpGet]
        public async Task Get(
            [FromRoute] long adId,
            [FromQuery(Name = "event")] string eventType,
            CancellationToken cancellationToken)
        {
            if (Enum.TryParse(eventType, true, out AdStats stats))
            {
                await Mediator.Send(new IncrementAdAssetsStatsCacheCommand(adId, stats), cancellationToken);
            }
        }
    }
}
