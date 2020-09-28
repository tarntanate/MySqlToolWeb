using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetRequest
    {
        public long AdId { get; set; }
        public AdAssetType AssetType { get; set; }
        public AdPosition Position { get; set; }
    }
}
