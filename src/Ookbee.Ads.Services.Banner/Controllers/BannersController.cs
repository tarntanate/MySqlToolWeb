
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Banner;
using Ookbee.Ads.Application.Business.Banner.Queries.GetListBanner;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Banner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<BannerDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetListBannerCommand(start, length));
    }
}
