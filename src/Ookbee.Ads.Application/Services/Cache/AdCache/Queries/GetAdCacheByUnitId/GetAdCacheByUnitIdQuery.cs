using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<Response<string>>
    {
        public Platform Platform { get; set; }
        public long AdUnitId { get; set; }
        public long? UserId { get; set; }

        public GetAdByUnitIdQuery(string platform, long adUnitId, long? userId)
        {
            Platform = EnumHelper.ConvertTo<Platform>(platform);
            AdUnitId = adUnitId;

            UserId = userId;
        }
    }
}
