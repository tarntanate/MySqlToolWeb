using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQuery : IRequest<HttpResult<AdAssetDto>>
    {
        public long AdId { get; set; }
        public Position Position { get; set; }

        public GetAdAssetByPositionQuery(long adId, Position position)
        {
            AdId = adId;
            Position = position;
        }
    }
}
