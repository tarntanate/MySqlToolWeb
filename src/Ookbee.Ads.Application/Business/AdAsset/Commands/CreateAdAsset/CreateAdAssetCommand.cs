using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommand : IRequest<HttpResult<long>>
    {
        public long AdId { get; set; }
        public string AssetType { get; set; }
        public string AssetPath { get; set; }
        public string Position { get; set; }

        public CreateAdAssetCommand()
        {

        }

        public CreateAdAssetCommand(CreateAdAssetCommand request)
        {
            AdId = request.AdId;
            AssetType = request.AssetType; // Enum.Parse(typeof(AssetType), request.AssetType);
            AssetPath = request.AssetPath;
            Position = request.Position;
        }
    }
}
