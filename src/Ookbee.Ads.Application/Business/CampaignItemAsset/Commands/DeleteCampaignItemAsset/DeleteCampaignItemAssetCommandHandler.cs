using MediatR;
using Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetByIdCampaignItemAsset;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Commands.DeleteCampaignItemAsset
{
    public class DeleteCampaignItemAssetCommandHandler : IRequestHandler<DeleteCampaignItemAssetCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public DeleteCampaignItemAssetCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();
            
            var campaignItemResult = await Mediatr.Send(new GetByIdCampaignItemAssetCommand(id));
            if (!campaignItemResult.Ok)
                return result.Fail(campaignItemResult.StatusCode, campaignItemResult.Message);

            await CampaignItemAssetMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
