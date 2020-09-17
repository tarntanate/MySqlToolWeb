using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdAssetByIdQuery(long id)
        {
            Id = id;
        }
    }
}
