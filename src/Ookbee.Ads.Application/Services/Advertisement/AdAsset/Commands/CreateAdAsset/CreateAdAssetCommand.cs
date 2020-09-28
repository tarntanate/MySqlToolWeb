using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommand : IRequest<Response<long>>
    {
        public long AdId { get; private set; }
        public AdAssetType AssetType { get; private set; }
        public AdPosition Position { get; private set; }

        public CreateAdAssetCommand(CreateAdAssetRequest request)
        {
            AdId = request.AdId;
            AssetType = request.AssetType;
            Position = request.Position;
        }
    }
}
