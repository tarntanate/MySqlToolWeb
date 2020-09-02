using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQuery : IRequest<HttpResult<bool>>
    {
        public long AdId { get; set; }
        public Position Position { get; set; }

        public IsExistsAdAssetByPositionQuery(long adId, Position position)
        {
            AdId = adId;
            Position = position;
        }
    }
}
