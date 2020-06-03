using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public UpdateCampaignCommandHandler(
            IMediator mediator,
            AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            Mediator = mediator;
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateCampaignCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(request.Id));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var isExistsAdvertiserResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(request.AdvertiserId));
                if (!isExistsAdvertiserResult.Ok)
                    return result.Fail(isExistsAdvertiserResult.StatusCode, isExistsAdvertiserResult.Message);

                var isExistsPricingModelResult = await Mediator.Send(new IsExistsPricingModelByIdQuery(request.PricingModelId));
                if (!isExistsPricingModelResult.Ok)
                    return result.Fail(isExistsPricingModelResult.StatusCode, isExistsPricingModelResult.Message);

                var adSlotResult = await Mediator.Send(new GetCampaignByNameQuery(request.Name));
                if (adSlotResult.Ok &&
                    adSlotResult.Data.Id != request.Id &&
                    adSlotResult.Data.AdvertiserId == request.AdvertiserId &&
                    adSlotResult.Data.PricingModelId == request.PricingModelId &&
                    adSlotResult.Data.Name == request.Name)
                    return result.Fail(409, $"Campaign '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(campaignResult.Data);
                var document = Mapper.Map(template).ToANew<CampaignDocument>();
                await CampaignMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
