using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public UpdateAdCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateAdCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var adResult = await Mediator.Send(new GetAdByIdQuery(request.Id));
                if (!adResult.Ok)
                    return result.Fail(adResult.StatusCode, adResult.Message);

                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(request.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var adSlotTypeResult = await Mediator.Send(new GetAdSlotByIdQuery(request.AdSlotId));
                if (!adSlotTypeResult.Ok)
                    return result.Fail(adSlotTypeResult.StatusCode, adSlotTypeResult.Message);

                var adByNameResult = await Mediator.Send(new GetAdByNameQuery(request.CampaignId, request.Name));
                if (adByNameResult.Ok &&
                    adByNameResult.Data.Id != request.Id &&
                    adByNameResult.Data.Name == request.Name)
                    return result.Fail(409, $"Ad '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(adResult.Data);
                var document = Mapper.Map(template).ToANew<AdDocument>();
                await AdMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
