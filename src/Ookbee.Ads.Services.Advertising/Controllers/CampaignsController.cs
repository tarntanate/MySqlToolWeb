
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertising.ViewModels;
using Ookbee.Ads.Application.Business.Advertising.Queries.GetCampaignList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : BaseController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignViewModel>>> Get([FromQuery]int start = 0, [FromQuery]int length = 10)
        {
            return await Mediator.Send(new GetCampaignListCommand
            {
                Start = start,
                Length = length
            });
        }
    }
}
