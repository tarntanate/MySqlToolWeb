using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommandHandler : IRequestHandler<CreateCampaignCostCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public CreateCampaignCostCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            CampaignCostDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<long>();
            var campaignId = 0L;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createCampaignResult = await CreateCampaignOnDb(request, cancellationToken);
                if (!createCampaignResult.Ok)
                    return result.Fail(createCampaignResult.StatusCode, createCampaignResult.Message);

                campaignId = createCampaignResult.Data;

                var createCampaignCostResult = await CreateCampaignCostOnDb(campaignId, request, cancellationToken);
                if (!createCampaignCostResult.Ok)
                    return result.Fail(createCampaignCostResult.StatusCode, createCampaignCostResult.Message);

                scope.Complete();
            }

            return result.Success(campaignId);
        }

        private async Task<HttpResult<long>> CreateCampaignOnDb(CreateCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var source = Mapper.Map<CreateCampaignRequest>(request);
            var command = new CreateCampaignCommand(source);
            return await Mediator.Send(command, cancellationToken);
        }

        private async Task<HttpResult<long>> CreateCampaignCostOnDb(long campaignId, CreateCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<CampaignCostEntity>(request);
            entity.CampaignId = campaignId;
            await CampaignCostDbRepo.InsertAsync(entity);
            await CampaignCostDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(entity.Id);
        }
    }
}
