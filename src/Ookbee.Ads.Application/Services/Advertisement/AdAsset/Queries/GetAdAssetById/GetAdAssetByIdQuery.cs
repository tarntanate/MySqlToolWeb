using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQuery : IRequest<Response<AdAssetDto>>
    {
        public long Id { get; private set; }

        public GetAdAssetByIdQuery(long id)
        {
            Id = id;
        }
    }
}
