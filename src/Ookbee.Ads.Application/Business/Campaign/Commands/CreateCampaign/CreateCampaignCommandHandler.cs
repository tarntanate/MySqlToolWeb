using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public CreateCampaignCommandHandler(
            IMediator mediator,
            AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
            Mediator = mediator;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateCampaignCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
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

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<CampaignDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignMongoDB.AddAsync(document);

                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
