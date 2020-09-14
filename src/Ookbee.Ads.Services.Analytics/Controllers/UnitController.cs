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
        public async Task UpdateUnitStats([FromRoute] long adUnitId, [FromQuery] string platform, [FromQuery] string type, CancellationToken cancellationToken)
            => await Mediator.Send(new IncrementAdUnitStatsCacheCommand(platform.ToEnum<Platform>(), type.ToEnum<StatsType>(), adUnitId), cancellationToken);
    }
}
