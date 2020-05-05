using MediatR;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.IsExistsByIdCampaignPricingModel;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.DeleteCampaignPricingModel
{
    public class DeleteCampaignPricingModelCommandHandler : IRequestHandler<DeleteCampaignPricingModelCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public DeleteCampaignPricingModelCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoDB)
        {
            Mediatr = mediatr;
            CampaignPricingModelMongoDB = campaignPricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdCampaignPricingModelCommand(id));
            if (!isExistsByNameResult.Data)
                return result.Fail(404, $"PricingModel '{id}' doesn't exist.");

            await CampaignPricingModelMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
