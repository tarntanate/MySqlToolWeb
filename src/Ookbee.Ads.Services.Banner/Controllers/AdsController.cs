using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Banner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ApiController
    {
        // [HttpGet]
        // public async Task<HttpResult<AdItemDto>> Get(
        //     [FromQuery] string adSlotId,
        //     [FromHeader(Name = "x-app-code")] string appCode,
        //     [FromHeader(Name = "x-app-version")] string appVersion,
        //     [FromHeader(Name = "x-device-id")] string deviceId,
        //     [FromHeader(Name = "x-device-platform")] string platform,
        //     [FromHeader(Name = "x-device-os")] string deviceOsVersion,
        //     [FromHeader(Name = "user-agent")] string userAgent)
        // {
        //     return await Mediator.Send(new GetAdBySlotIdQuery(appCode, appVersion, adSlotId, deviceId, platform, userAgent));
        // }
    }
}
