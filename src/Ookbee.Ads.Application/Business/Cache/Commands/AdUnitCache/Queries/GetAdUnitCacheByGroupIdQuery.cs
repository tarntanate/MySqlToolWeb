using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }

        public GetAdUnitCacheByGroupIdQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
