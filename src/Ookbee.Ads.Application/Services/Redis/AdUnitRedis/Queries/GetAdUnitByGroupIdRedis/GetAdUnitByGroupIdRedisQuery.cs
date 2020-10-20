using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupIdRedis
{
    public class GetAdUnitByGroupIdRedisQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; private set; }
        public long AdGroupId { get; private set; }

        public GetAdUnitByGroupIdRedisQuery(string platform, long adGroupId)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdGroupId = adGroupId;
        }
    }
}
