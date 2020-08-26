using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }

        public GetAdUnitCacheByGroupIdQuery(long adGroupId, Platform platform)
        {
            AdGroupId = adGroupId;
            Platform = platform;
        }
    }
}
