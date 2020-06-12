using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommandHandler : IRequestHandler<CreateCampaignCostCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public CreateCampaignCostCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            CampaignCostDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateCampaignCostCommand request)
        {
            var result = new HttpResult<long>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createCampaignResult = await CreateCampaignOnDb(request);
                if (!createCampaignResult.Ok)
                    return result.Fail(createCampaignResult.StatusCode, createCampaignResult.Message);

                var createCampaignCostResult = await CreateCampaignCostOnDb(createCampaignResult.Data, request);
                if (!createCampaignCostResult.Ok)
                    return result.Fail(createCampaignCostResult.StatusCode, createCampaignCostResult.Message);

                scope.Complete();

                return result.Success(createCampaignResult.Data);
            }
        }

        private async Task<HttpResult<long>> CreateCampaignOnDb(CreateCampaignCostCommand request)
        {
            var command = Mapper
                .Map(request)
                .ToANew<CreateCampaignCommand>();

            var result = await Mediator.Send(command);

            return result;
        }

        private async Task<HttpResult<long>> CreateCampaignCostOnDb(long campaignId, CreateCampaignCostCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper
                .Map(request)
                .ToANew<CampaignCostEntity>(cfg =>
                    cfg.Ignore(m => m.Campaign));

            entity.CampaignId = campaignId;

            await CampaignCostDbRepo.InsertAsync(entity);
            await CampaignCostDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
