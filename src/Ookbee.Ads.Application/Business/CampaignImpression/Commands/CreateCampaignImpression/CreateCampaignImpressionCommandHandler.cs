using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommandHandler : IRequestHandler<CreateCampaignImpressionCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public CreateCampaignImpressionCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateCampaignImpressionCommand request)
        {
            var result = new HttpResult<long>();
            var campaignId = 0L;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createCampaignResult = await CreateCampaignOnDb(request);
                if (!createCampaignResult.Ok)
                    return result.Fail(createCampaignResult.StatusCode, createCampaignResult.Message);

                campaignId = createCampaignResult.Data;

                var createCampaignImpressionResult = await CreateCampaignImpressionOnDb(campaignId, request);
                if (!createCampaignImpressionResult.Ok)
                    return result.Fail(createCampaignImpressionResult.StatusCode, createCampaignImpressionResult.Message);

                scope.Complete();
            }

            return result.Success(campaignId);
        }

        private async Task<HttpResult<long>> CreateCampaignOnDb(CreateCampaignImpressionCommand request)
        {
            var source = Mapper.Map<CreateCampaignRequest>(request);
            var command = new CreateCampaignCommand(source);
            return await Mediator.Send(command);
        }

        private async Task<HttpResult<long>> CreateCampaignImpressionOnDb(long campaignId, CreateCampaignImpressionCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<CampaignImpressionEntity>(request);
            entity.CampaignId = campaignId;
            await CampaignImpressionDbRepo.InsertAsync(entity);
            await CampaignImpressionDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
