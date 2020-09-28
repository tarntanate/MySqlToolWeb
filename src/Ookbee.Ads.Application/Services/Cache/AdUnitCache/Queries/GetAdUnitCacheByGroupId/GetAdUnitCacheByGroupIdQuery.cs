using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; private set; }
        public long AdGroupId { get; private set; }


        public GetAdUnitCacheByGroupIdQuery(string platform, long adGroupId)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdGroupId = adGroupId;
        }
    }
}
