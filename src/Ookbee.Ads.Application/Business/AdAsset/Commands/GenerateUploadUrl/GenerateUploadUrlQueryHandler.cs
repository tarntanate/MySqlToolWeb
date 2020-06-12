using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandHandler : IRequestHandler<GenerateUploadUrlCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GenerateUploadUrlCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<string>> Handle(GenerateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id));
            if (!adAssetResult.Ok)
                return result.Fail(adAssetResult.StatusCode, adAssetResult.Message);

            var adAsset = adAssetResult.Data;
            var bucket = GlobalVar.AppSettings.Tencent.Cos.Bucket.Private;
            var targetKey = $"/ads/{adAsset.AdId}/assets/{adAsset.Id}{request.Extension}";
            var sourceKey = $"temp{targetKey}";

            var generateSignURLCommand = new GenerateSignURLCommand()
            {
                Bucket = bucket,
                Key = sourceKey,
            };
            var signURLResult = await Mediator.Send(generateSignURLCommand);
            if (!signURLResult.Ok)
                return result.Fail(signURLResult.StatusCode, signURLResult.Message);

            var assetMimeType = MimeTypeMap.GetMimeType(request.Extension);
            adAsset.AssetPath = targetKey;
            adAsset.AssetType = assetMimeType.Split('/')[0].ToUpperFirstLetter();
            var updateAdAssetCommand = Mapper.Map(adAsset).ToANew<UpdateAdAssetCommand>();
            var updateAdAssetResult = await Mediator.Send(updateAdAssetCommand);
            if (!updateAdAssetResult.Ok)
                return result.Fail(updateAdAssetResult.StatusCode, updateAdAssetResult.Message);

            return result.Success(signURLResult.Data);
        }
    }
}
