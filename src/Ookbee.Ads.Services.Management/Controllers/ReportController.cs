using Microsoft.AspNetCore.Mvc;

using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        [HttpGet("group")]
        public async Task<HttpResult<IEnumerable<UserDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
           => await Mediator.Send(new GetUserListQuery(start, length), cancellationToken);

        [HttpGet("group/{id}")]
        public async Task<HttpResult<UserDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);       
    }
}
