using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommand : UpdateAdAssetRequest, IRequest<HttpResult<bool>>
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
