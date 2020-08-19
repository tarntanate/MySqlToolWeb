using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetworkUnit.Queries.GetAdNetworkUnitByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class AdNetworkUnitsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdNetworkUnitByUnitId([FromRoute] long unitId, [FromQuery] string platform)
        {
            var result = await Mediator.Send(new GetAdNetworkUnitByUnitIdQuery(unitId, platform));
            return Content(result.Data, "application/json");
        }
    }
}
