using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQuery : IRequest<HttpResult<bool>>
    {
        public Position Position { get; set; }

        public IsExistsAdAssetByPositionQuery(Position position)
        {
            Position = position;
        }
    }
}
