using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.IsExistsByIdCampaignPricingModel;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.UpdateCampaignPricingModel
{
    public class UpdateCampaignPricingModelCommandHandler : IRequestHandler<UpdateCampaignPricingModelCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public UpdateCampaignPricingModelCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoDB)
        {
            Mediatr = mediatr;
            CampaignPricingModelMongoDB = campaignPricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignPricingModelDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(CampaignPricingModelDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdCampaignPricingModelCommand(document.Id));
                if (!isExistsByNameResult.Data)
                    return result.Fail(404, $"PricingModel '{document.Id}' doesn't exist.");

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignPricingModelMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
