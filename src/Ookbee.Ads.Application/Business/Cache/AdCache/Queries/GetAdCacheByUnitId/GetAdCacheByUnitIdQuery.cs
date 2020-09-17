using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }

        public GetAdByUnitIdQuery(long adUnitId, Platform platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
