using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<Response<string>>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }

        public GetAdByUnitIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = EnumHelper.ConvertTo<Platform>(platform);
        }
    }
}
