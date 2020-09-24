using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommand : IRequest<Response<long>>
    {
        public long AdId { get; private set; }
        public AssetType AssetType { get; private set; }
        public Position Position { get; private set; }

        public CreateAdAssetCommand(long adId, AssetType assetType, Position position)
        {
            AdId = adId;
            AssetType = assetType;
            Position = position;
        }

        public CreateAdAssetCommand(CreateAdAssetRequest request)
        {
            AdId = request.AdId;
            AssetType = request.AssetType;
            Position = request.Position;
        }
    }
}
