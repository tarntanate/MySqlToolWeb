using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeById;
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
            var document = Mapper.Map(request).ToANew<AdDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(AdDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(document.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var slotTypeResult = await Mediator.Send(new GetSlotTypeByIdQuery(document.AdSlotId));
                if (!slotTypeResult.Ok)
                    return result.Fail(slotTypeResult.StatusCode, slotTypeResult.Message);

                var now = MechineDateTime.Now;
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
