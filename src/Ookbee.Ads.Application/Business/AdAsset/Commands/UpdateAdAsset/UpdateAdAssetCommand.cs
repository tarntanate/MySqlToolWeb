using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public AssetType AssetType { get; set; }
        public string AssetPath { get; set; }
        public Position Position { get; set; }

        public UpdateAdAssetCommand()
        {

        }

        public UpdateAdAssetCommand(long id, UpdateAdAssetCommand request)
        {
            Id = request.Id;
            AdId = request.AdId;
            AssetType = request.AssetType;
            AssetPath = request.AssetPath;
            Position = request.Position;
        }
    }
}
