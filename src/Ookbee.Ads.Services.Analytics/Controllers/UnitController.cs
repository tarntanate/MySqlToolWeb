using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adUnitId}/stats")]
    public class UnitsController : ApiController
    {
        [HttpGet]
        public async Task<ContentResult> UpdateUnitStats([FromRoute] long adUnitId, [FromQuery] string platform, [FromQuery] string type, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IncrementAdUnitStatsCacheCommand(type.ToEnum<StatsType>(), adUnitId), cancellationToken);
            if (result.Ok &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
