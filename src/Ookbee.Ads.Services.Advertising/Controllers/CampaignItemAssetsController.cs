
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.CampaignItemType;
using Ookbee.Ads.Application.Business.CampaignItemType.Commands.CreateCampaignItemType;
using Ookbee.Ads.Application.Business.CampaignItemType.Commands.DeleteCampaignItemType;
using Ookbee.Ads.Application.Business.CampaignItemType.Commands.UpdateCampaignItemType;
using Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetByIdCampaignItemType;
using Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetListCampaignItemType;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/items/assets")]
    public class CampaignItemAssetsController : BaseController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignItemTypeDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignItemTypeCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignItemTypeDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdCampaignItemTypeCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateCampaignItemTypeCommand request)
            => await Mediator.Send(new CreateCampaignItemTypeCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateCampaignItemTypeCommand request)
            => await Mediator.Send(new UpdateCampaignItemTypeCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteCampaignItemTypeCommand(id));
    }
}
