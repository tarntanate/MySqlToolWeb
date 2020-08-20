﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class AdNetworkUnitsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdByUnitId([FromRoute] long unitId, [FromQuery] string platform)
        {
            var result = await Mediator.Send(new GetAdByUnitIdQuery(unitId, platform));
            if (result.Ok)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = (int)result.StatusCode };
        }
    }
}
