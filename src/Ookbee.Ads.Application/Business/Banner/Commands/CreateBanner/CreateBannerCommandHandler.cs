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

namespace Ookbee.Ads.Application.Business.Banner.Commands.CreateBanner
{
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<BannerDocument> BannerMongoDB { get; }

        public CreateBannerCommandHandler(
            IMediator mediator,
            AdsMongoRepository<BannerDocument> bannerMongoDB)
        {
            Mediator = mediator;
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<BannerDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(BannerDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(document.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var slotTypeResult = await Mediator.Send(new GetSlotTypeByIdQuery(document.SlotTypeId));
                if (!slotTypeResult.Ok)
                    return result.Fail(slotTypeResult.StatusCode, slotTypeResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await BannerMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
