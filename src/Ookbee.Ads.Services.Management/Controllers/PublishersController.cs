using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.CreatePublisher;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.DeletePublisher;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.UpdatePublisher;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherById;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<PublisherDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetPublisherListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<PublisherDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetPublisherByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreatePublisherRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreatePublisherCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdatePublisherRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdatePublisherCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeletePublisherCommand(id), cancellationToken);
    }
}
