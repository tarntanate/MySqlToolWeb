using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.GetAdAssetByUnitId
{
    public class GetAdAssetByUnitIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }

        public GetAdAssetByUnitIdQuery(long adUnitId, Platform platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
