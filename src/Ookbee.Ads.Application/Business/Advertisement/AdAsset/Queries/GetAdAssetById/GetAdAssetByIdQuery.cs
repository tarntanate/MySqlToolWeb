using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQuery : IRequest<HttpResult<AdAssetDto>>
    {
        public long Id { get; set; }

        public GetAdAssetByIdQuery(long id)
        {
            Id = id;
        }
    }
}
