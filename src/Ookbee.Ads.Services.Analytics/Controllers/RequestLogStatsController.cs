using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Analytics.Commands.UpdateRequestLogEvent;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.AspNetCore.Controllers;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/events/{eventId}/{eventType}")]
    public class RequestLogStatsController : ApiController
    {
        [HttpGet]
        public async Task Get([FromRoute] long eventId, [FromRoute] string eventType)
            => await Mediator.Send(new UpdateRequestLogEventCommand(eventId, eventType));
    }
}
