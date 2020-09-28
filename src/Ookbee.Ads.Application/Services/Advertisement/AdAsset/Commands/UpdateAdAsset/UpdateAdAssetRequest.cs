using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetRequest
    {
        public long AdId { get; set; }
        public string AssetPath { get; set; }
        public AdAssetType AssetType { get; set; }
        public AdPosition Position { get; set; }
    }
}
