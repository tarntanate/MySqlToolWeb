
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.CampaignItem;
using Ookbee.Ads.Application.Business.CampaignItem.Commands.CreateCampaignItem;
using Ookbee.Ads.Application.Business.CampaignItem.Commands.DeleteCampaignItem;
using Ookbee.Ads.Application.Business.CampaignItem.Commands.UpdateCampaignItem;
using Ookbee.Ads.Application.Business.CampaignItem.Queries.GetByIdCampaignItem;
using Ookbee.Ads.Application.Business.CampaignItem.Queries.GetListCampaignItem;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/items")]
    public class CampaignItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignItemDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignItemCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignItemDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdCampaignItemCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateCampaignItemCommand request)
            => await Mediator.Send(new CreateCampaignItemCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateCampaignItemCommand request)
            => await Mediator.Send(new UpdateCampaignItemCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteCampaignItemCommand(id));
    }
}
