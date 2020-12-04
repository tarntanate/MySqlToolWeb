using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorld;
using Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorldRedis;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/load-testing")]
    public class LoadTestingController : ApiController
    {
        [HttpGet("hello-world-1")]
        public async Task<ContentResult> GetValueOnController(
            [FromQuery] AdPlatform platform,
            [FromRoute] long groupId,
            [FromQuery] string ookbeeId,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId_header,
            CancellationToken cancellationToken)
        {
            var redisValues = string.Empty;
            await Task.Run(() => { redisValues = "{\"test\": \"HelloWorld\"}"; });
            return Content(redisValues, "application/json");
        }

        [HttpGet("hello-world-2")]
        public async Task<ContentResult> GetValueOnMediatR(
            [FromQuery] AdPlatform platform,
            [FromRoute] long groupId,
            [FromQuery] string ookbeeId,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId_header,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetHelloWorldQuery(), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }

        [HttpGet("hello-world-3")]
        public async Task<ContentResult> GetValueOnRedis(
            [FromQuery] AdPlatform platform,
            [FromRoute] long groupId,
            [FromQuery] string ookbeeId,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId_header,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetHelloWorldRedisQuery(), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
