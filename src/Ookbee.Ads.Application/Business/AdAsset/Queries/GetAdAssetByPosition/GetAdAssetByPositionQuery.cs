using MediatR;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQuery : IRequest<HttpResult<AdAssetDto>>
    {
        public Position Position { get; set; }

        public GetAdAssetByPositionQuery(Position position)
        {
            Position = position;
        }
    }
}
