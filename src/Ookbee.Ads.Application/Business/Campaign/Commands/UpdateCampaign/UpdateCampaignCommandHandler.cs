using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public UpdateCampaignCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo)
        {
            Mediator = mediator;
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateCampaignCommand request)
        {
            var result = new HttpResult<bool>();

            var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(request.Id));
            if (!campaignResult.Ok)
                return result.Fail(campaignResult.StatusCode, campaignResult.Message);

            if (campaignResult.Ok &&
                campaignResult.Data.PricingModel != request.PricingModel.ToString())
                return result.Fail(401, $"You don't have permission to change the Pricing Model.");

            var campaignByNameResult = await Mediator.Send(new GetCampaignByNameQuery(request.Name));
            if (campaignByNameResult.Ok &&
                campaignByNameResult.Data.Id != request.Id &&
                campaignByNameResult.Data.Name == request.Name &&
                campaignByNameResult.Data.PricingModel == request.PricingModel.ToString())
                return result.Fail(409, $"Campaign '{request.Name}' already exists.");

            var isExistsAdvertiserResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(request.AdvertiserId));
            if (!isExistsAdvertiserResult.Ok)
                return result.Fail(isExistsAdvertiserResult.StatusCode, isExistsAdvertiserResult.Message);

            var entity = Mapper
            .Map(request)
                .ToANew<CampaignEntity>(cfg =>
                    cfg.Ignore(
                        m => m.Advertiser,
                        m => m.CampaignCost,
                        m => m.CampaignImpression));

            await CampaignEFCoreRepo.UpdateAsync(entity.Id, entity);
            await CampaignEFCoreRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
