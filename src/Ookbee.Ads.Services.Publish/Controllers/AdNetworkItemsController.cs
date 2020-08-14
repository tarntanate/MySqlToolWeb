﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetworkItem.Queries.GetAdNetworkItemByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class AdNetworkItemsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdNetworkItemByUnitId([FromRoute] long unitId, [FromQuery] string platform)
        {
            var result = await Mediator.Send(new GetAdNetworkItemByUnitIdQuery(unitId, platform));
            return Content(result.Data, "application/json");
        }
    }
}
