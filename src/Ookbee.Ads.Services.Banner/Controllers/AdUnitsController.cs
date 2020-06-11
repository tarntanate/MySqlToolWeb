using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByAdUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Banner.Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdUnitsController : ApiController
    {
        [HttpGet("{adUnitId}/banner")]
        public async Task<HttpResult<BannerDto>> GetById([FromRoute] long adUnitId)
            => await Mediator.Send(new GetAdByAdUnitIdQuery(adUnitId));
    }
}
