using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdAssetByIdQuery(long id)
        {
            Id = id;
        }
    }
}
