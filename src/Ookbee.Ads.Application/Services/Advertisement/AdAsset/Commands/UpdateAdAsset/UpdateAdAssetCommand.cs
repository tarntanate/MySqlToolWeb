using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdId { get; private set; }
        public string AssetPath { get; private set; }
        public AssetType AssetType { get; private set; }
        public Position Position { get; private set; }

        public UpdateAdAssetCommand(long id, long adId, string assetPath, AssetType assetType, Position position)
        {
            Id = id;
            AdId = adId;
            AssetPath = assetPath;
            AssetType = assetType;
            Position = position;
        }

        public UpdateAdAssetCommand(long id, UpdateAdAssetRequest request)
        {
            Id = id;
            AdId = request.AdId;
            AssetPath = request.AssetPath;
            AssetType = request.AssetType;
            Position = request.Position;
        }
    }
}
