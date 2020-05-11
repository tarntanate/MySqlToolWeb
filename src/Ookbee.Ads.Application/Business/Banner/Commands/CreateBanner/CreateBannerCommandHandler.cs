using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetByIdCampaign;
using Ookbee.Ads.Application.Business.UnitType.Queries.GetByIdUnitType;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Commands.CreateBanner
{
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public CreateBannerCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            Mediatr = mediatr;
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
                var campaignResult = await Mediatr.Send(new GetByIdCampaignCommand(document.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var UnitTypeResult = await Mediatr.Send(new GetByIdUnitTypeCommand(document.CampaignId));
                if (!UnitTypeResult.Ok)
                    return result.Fail(UnitTypeResult.StatusCode, UnitTypeResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await BannerMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
