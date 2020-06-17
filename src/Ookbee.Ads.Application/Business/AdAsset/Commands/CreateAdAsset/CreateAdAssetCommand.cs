using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommand : IRequest<HttpResult<long>>
    {
        public long AdId { get; set; }
        public AssetType AssetType { get; set; }
        public string AssetPath { get; set; }
        public Position Position { get; set; }

        public CreateAdAssetCommand()
        {

        }

        public CreateAdAssetCommand(CreateAdAssetCommand request)
        {
            AdId = request.AdId;
            AssetType = request.AssetType;
            AssetPath = request.AssetPath;
            Position = request.Position;
        }
    }
}
