using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.GetAdAssetByUnitId
{
    public class GetAdAssetByUnitIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public string Platform { get; set; }

        public GetAdAssetByUnitIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
