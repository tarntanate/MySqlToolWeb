using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Banner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignItemTypeDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignItemTypeCommand(start, length));
    }
}
