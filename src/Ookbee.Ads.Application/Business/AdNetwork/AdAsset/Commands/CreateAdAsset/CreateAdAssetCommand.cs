using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.CreateAdAsset
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
