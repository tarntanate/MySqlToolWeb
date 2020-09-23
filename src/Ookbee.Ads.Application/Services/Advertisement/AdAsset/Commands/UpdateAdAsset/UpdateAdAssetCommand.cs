using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommand : UpdateAdAssetRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

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
