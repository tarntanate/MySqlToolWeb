using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQuery : IRequest<Response<AdAssetDto>>
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
