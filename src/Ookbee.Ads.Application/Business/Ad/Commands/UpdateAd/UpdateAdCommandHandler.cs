using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Common.Helpers;
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
                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(request.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.CampaignId, request.Id));
                if (!isExistsAdResult.Ok)
                    return isExistsAdResult;

                var isExistsAdByNameResult = await Mediator.Send(new IsExistsAdByNameQuery(request.AdSlotId, request.Name));
                if (isExistsAdByNameResult.Data)
                    return result.Fail(409, $"Ad '{request.Name}' already exists.");

                var adSlotTypeResult = await Mediator.Send(new GetAdSlotByIdQuery(request.AdSlotId));
                if (!adSlotTypeResult.Ok)
                    return result.Fail(adSlotTypeResult.StatusCode, adSlotTypeResult.Message);
                    
                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<AdDocument>();
                document.UpdatedDate = now.DateTime;
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
