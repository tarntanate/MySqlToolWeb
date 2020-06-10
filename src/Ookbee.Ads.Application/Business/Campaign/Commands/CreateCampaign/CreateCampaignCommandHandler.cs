using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, HttpResult<long>>
    {
        public CreateCampaignCommandHandler(IMediator mediator)
        {
            this.Mediator = mediator;

        }
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public CreateCampaignCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo)
        {
            Mediator = mediator;
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateCampaignCommand request)
        {
            var result = new HttpResult<long>();

            var advertiserResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(request.AdvertiserId));
            if (!advertiserResult.Ok)
                return result.Fail(advertiserResult.StatusCode, advertiserResult.Message);

            var campaignByNameResult = await Mediator.Send(new GetCampaignByNameQuery(request.Name));
            if (campaignByNameResult.Ok &&
                campaignByNameResult.Data.Name == request.Name &&
                campaignByNameResult.Data.PricingModel == request.PricingModel.ToString())
                return result.Fail(409, $"Campaign '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<CampaignEntity>(cfg =>
                    cfg.Ignore(
                        m => m.Advertiser,
                        m => m.CampaignCost,
                        m => m.CampaignImpression));

            await CampaignEFCoreRepo.InsertAsync(entity);
            await CampaignEFCoreRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
