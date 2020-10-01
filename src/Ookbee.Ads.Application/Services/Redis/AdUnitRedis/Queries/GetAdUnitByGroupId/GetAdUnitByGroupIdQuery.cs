using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupId
{
    public class GetAdUnitByGroupIdQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; private set; }
        public long AdGroupId { get; private set; }

        public GetAdUnitByGroupIdQuery(string platform, long adGroupId)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdGroupId = adGroupId;
        }
    }
}
