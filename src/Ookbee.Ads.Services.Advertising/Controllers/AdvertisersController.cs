
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.CreateAdvertiser;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.DeleteAdvertiser;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetAdvertiser;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetListAdvertiser;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.ViewModels;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisersController : BaseController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdvertiserViewModel>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListAdvertiserCommand(start, length) { });

        [HttpGet("{id}")]
        public async Task<HttpResult<AdvertiserViewModel>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetAdvertiserCommand(id) { });

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateAdvertiserCommand request)
            => await Mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteAdvertiserCommand(id) { });
    }
}
