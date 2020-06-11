
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
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager .Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<PublisherDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetPublisherListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<PublisherDto>> GetById([FromRoute]long id)
            => await Mediator.Send(new GetPublisherByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody]CreatePublisherCommand request)
            => await Mediator.Send(new CreatePublisherCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]long id, [FromBody]UpdatePublisherCommand request)
            => await Mediator.Send(new UpdatePublisherCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]long id)
            => await Mediator.Send(new DeletePublisherCommand(id));
    }
}
