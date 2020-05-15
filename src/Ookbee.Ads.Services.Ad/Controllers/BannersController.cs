
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Ad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdListQuery(start, length));
    }
}
