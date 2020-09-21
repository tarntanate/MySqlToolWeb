using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommand : CreateAdAssetRequest, IRequest<HttpResult<long>>
    {
        public CreateAdAssetCommand(CreateAdAssetRequest request)
        {
            AdId = request.AdId;
            AssetType = request.AssetType;
            Position = request.Position;
        }
    }
}
