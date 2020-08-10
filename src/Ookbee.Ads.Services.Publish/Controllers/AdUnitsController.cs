using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Banner;
using Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdUnitsController : ApiController
    {
        [HttpGet("{adUnitId}")]
        public async Task<HttpResult<BannerDto>> GetById(
            [FromRoute] long adUnitId,
            [FromHeader(Name = "Ookbee-App-Code")] string appCode,
            [FromHeader(Name = "Ookbee-App-Version")] string appVersion,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId,
            [FromHeader(Name = "Ookbee-Device-OS")] string deviceOs,
            [FromHeader(Name = "Ookbee-Platform")] string platform,
            [FromHeader(Name = "User-Agent")] string userAgent)
            => await Mediator.Send(new GetBannerByAdUnitIdQuery(adUnitId, appCode, appVersion, deviceId, deviceOs, platform, userAgent));
    }
}
