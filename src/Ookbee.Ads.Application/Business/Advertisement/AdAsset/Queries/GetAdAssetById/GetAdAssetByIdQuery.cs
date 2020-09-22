using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQuery : IRequest<Response<AdAssetDto>>
    {
        public long Id { get; set; }

        public GetAdAssetByIdQuery(long id)
        {
            Id = id;
        }
    }
}
