using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog;
using Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
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
        public async Task<ContentResult> GetAdUnitCacheByGroupId([FromRoute] long groupId, [FromQuery] AdPlatform platform, CancellationToken cancellationToken)
        {
            // For Testing TimeScaleDb
            var timescaleResult = await Mediator.Send(
                new CreateGroupRequestLogCommand(
                    platformId: (short)platform,
                    adGroupId: (short)groupId,
                    uuid: new Random().Next(0, 20).ToString()),
                    cancellationToken);

            string platformString = Enum.GetName(typeof(AdPlatform), platform);

            var result = await Mediator.Send(new GetAdUnitByGroupIdQuery(platformString, groupId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
