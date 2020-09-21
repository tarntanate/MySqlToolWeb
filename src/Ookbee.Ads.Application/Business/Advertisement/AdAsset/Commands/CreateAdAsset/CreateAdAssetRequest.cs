using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetRequest
    {
        public long AdId { get; set; }
        public AssetType AssetType { get; set; }
        public Position Position { get; set; }
    }
}
