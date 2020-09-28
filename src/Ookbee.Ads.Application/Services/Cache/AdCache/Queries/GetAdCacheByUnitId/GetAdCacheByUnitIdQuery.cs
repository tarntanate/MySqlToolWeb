using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; set; }
        public long AdUnitId { get; set; }
        public long? UserId { get; set; }

        public GetAdByUnitIdQuery(string platform, long adUnitId, long? userId)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdUnitId = adUnitId;

            UserId = userId;
        }
    }
}
