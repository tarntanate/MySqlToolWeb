using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommandHandler : IRequestHandler<CreateCampaignImpressionCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignImpressionEntity> CampaignImpressionEFCoreRepo { get; }

        public CreateCampaignImpressionCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignImpressionEntity> advertiserEFCoreRepo)
        {
            Mediator = mediator;
            CampaignImpressionEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateCampaignImpressionCommand request)
        {
            var result = new HttpResult<long>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createCampaignResult = await CreateCampaignOnDb(request);
                if (!createCampaignResult.Ok)
                    return result.Fail(createCampaignResult.StatusCode, createCampaignResult.Message);

                var createCampaignImpressionResult = await CreateCampaignImpressionOnDb(createCampaignResult.Data, request);
                if (!createCampaignImpressionResult.Ok)
                    return result.Fail(createCampaignImpressionResult.StatusCode, createCampaignImpressionResult.Message);

                scope.Complete();

                return result.Success(createCampaignResult.Data);
            }
        }

        private async Task<HttpResult<long>> CreateCampaignOnDb(CreateCampaignImpressionCommand request)
        {
            var command = Mapper
                .Map(request)
                .ToANew<CreateCampaignCommand>();

            var result = await Mediator.Send(command);

            return result;
        }

        private async Task<HttpResult<long>> CreateCampaignImpressionOnDb(long campaignId, CreateCampaignImpressionCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper
                .Map(request)
                .ToANew<CampaignImpressionEntity>(cfg =>
                    cfg.Ignore(m => m.Campaign));

            entity.CampaignId = campaignId;

            await CampaignImpressionEFCoreRepo.InsertAsync(entity);
            await CampaignImpressionEFCoreRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
