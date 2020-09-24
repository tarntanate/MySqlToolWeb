﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdUnitCacheByGroupId([FromQuery] string platform, [FromRoute] long groupId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdUnitCacheByGroupIdQuery(platform, groupId), cancellationToken);
            if (result.Ok &&
                result.Data.HasValue())
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
