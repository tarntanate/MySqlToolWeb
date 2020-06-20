using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Publisher;
using Ookbee.Ads.Application.Business.Publisher.Commands.CreatePublisher;
using Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher;
using Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<PublisherDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetPublisherListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<PublisherDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetPublisherByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreatePublisherRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreatePublisherCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdatePublisherCommand request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdatePublisherCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeletePublisherCommand(id), cancellationToken);
    }
}
