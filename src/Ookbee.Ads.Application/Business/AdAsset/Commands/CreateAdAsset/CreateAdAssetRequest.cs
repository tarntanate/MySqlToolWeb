using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetRequest
    {
        public long AdId { get; set; }
        public AssetType AssetType { get; set; }
        public string AssetPath { get; set; }
        public Position Position { get; set; }
    }
}
