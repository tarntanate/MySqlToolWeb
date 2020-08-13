using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Ookbee.Ads.Application.Business.ActivityLog.Queries.GetActivityLogList;
using Ookbee.Ads.Application.Business.ActivityLog;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivityController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<ActivityLogDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? userId, [FromQuery] string objectName, CancellationToken cancellationToken)
            => await Mediator.Send(new GetActivityLogListQuery(start, length, userId, objectName), cancellationToken);
    }
}
