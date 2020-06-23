using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetRequest
    {
        public long AdId { get; set; }
        public AssetType AssetType { get; set; }
        public Position Position { get; set; }
    }
}
