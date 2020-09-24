using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQuery : IRequest<Response<bool>>
    {
        public long AdId { get; private set; }
        public AdPosition Position { get; private set; }

        public IsExistsAdAssetByPositionQuery(long adId, AdPosition position)
        {
            AdId = adId;
            Position = position;
        }
    }
}
