﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdUnitCacheByGroupId([FromRoute] long groupId, [FromQuery] string platform, CancellationToken cancellationToken)
        {
            if (Enum.TryParse(platform, true, out Platform platformx))
            {
                // For Testing TimeScaleDb
                var timescaleResult = await Mediator.Send(
                    new CreateGroupRequestLogCommand(
                        platformId: (short) platformx,
                        adGroupId: (short) groupId,
                        uuid: new Random().Next(0,20).ToString()),
                        cancellationToken);

                var result = await Mediator.Send(new GetAdUnitCacheByGroupIdQuery(groupId, platformx), cancellationToken);
                return Content(result.Data, "application/json");
            }
            return new ContentResult() { StatusCode = 400 };
        }
    }
}
