using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandHandler : IRequestHandler<GenerateUploadUrlCommand, Response<string>>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;

        public GenerateUploadUrlCommandHandler(
            IMapper mapper,
            IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

        public async Task<Response<string>> Handle(GenerateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<string>();

            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id), cancellationToken);
            if (!adAssetResult.IsSuccess)
                return result.Status(adAssetResult.StatusCode, adAssetResult.Message);

            var adAsset = adAssetResult.Data;
            var bucket = GlobalVar.AppSettings.Tencent.Cos.Bucket.Private;
            var targetKey = $"/ads/{adAsset.AdId}/assets/{adAsset.Id}{request.Extension}";
            var sourceKey = $"temp{targetKey}";

            var generateSignURLCommand = new GenerateSignURLCommand()
            {
                Bucket = bucket,
                Key = sourceKey,
            };
            var signURLResult = await Mediator.Send(generateSignURLCommand, cancellationToken);
            if (!signURLResult.IsSuccess)
                return result.Status(signURLResult.StatusCode, signURLResult.Message);

            var assetMimeType = MimeTypeMap.GetMimeType(request.Extension);
            adAsset.AssetPath = targetKey;
            adAsset.AssetType = assetMimeType.Split('/')[0].ToUpperFirstLetter();
            var updateAdAssetRequest = Mapper.Map<UpdateAdAssetRequest>(adAsset);
            var updateAdAssetCommand = new UpdateAdAssetCommand(adAsset.Id, updateAdAssetRequest);
            var updateAdAssetResult = await Mediator.Send(updateAdAssetCommand, cancellationToken);
            if (!updateAdAssetResult.IsSuccess)
                return result.Status(updateAdAssetResult.StatusCode, updateAdAssetResult.Message);

            return result.OK(signURLResult.Data);
        }
    }
}
