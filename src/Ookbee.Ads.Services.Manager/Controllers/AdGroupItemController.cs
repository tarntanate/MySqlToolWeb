
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdGroupItem;
using Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem;
using Ookbee.Ads.Application.Business.AdGroupItem.Commands.DeleteAdGroupItem;
using Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemById;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdGroupItemController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdGroupItemDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adGroupId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupItemListQuery(start, length, adGroupId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<AdGroupItemDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupItemByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdGroupItemRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdGroupItemCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdGroupItemRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdGroupItemCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdGroupItemCommand(id), cancellationToken);
    }
}
