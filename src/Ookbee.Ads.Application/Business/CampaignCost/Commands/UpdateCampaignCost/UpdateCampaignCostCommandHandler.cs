using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Application.Business.CampaignCost.Queries.GetCampaignCostById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.UpdateCampaignCost
{
    public class UpdateCampaignCostCommandHandler : IRequestHandler<UpdateCampaignCostCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public UpdateCampaignCostCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            CampaignCostDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateCampaignCostCommand request)
        {
            var result = new HttpResult<bool>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updateCampaignResult = await UpdateCampaignOnDb(request);
                if (!updateCampaignResult.Ok)
                    return result.Fail(updateCampaignResult.StatusCode, updateCampaignResult.Message);

                var updateCampaignCostResult = await UpdateCampaignCostOnDb(request);
                if (!updateCampaignCostResult.Ok)
                    return result.Fail(updateCampaignCostResult.StatusCode, updateCampaignCostResult.Message);

                scope.Complete();

                return result.Success(true);
            }
        }

        private async Task<HttpResult<bool>> UpdateCampaignOnDb(UpdateCampaignCostCommand request)
        {
            var command = Mapper
                .Map(request)
                .ToANew<UpdateCampaignCommand>();

            var result = await Mediator.Send(command);

            return result;
        }

        private async Task<HttpResult<bool>> UpdateCampaignCostOnDb(UpdateCampaignCostCommand request)
        {
            var result = new HttpResult<bool>();

            var campaignCostResult = await Mediator.Send(new GetCampaignCostByCampaignIdQuery(request.Id));
            if (!campaignCostResult.Ok)
                return result.Fail(campaignCostResult.StatusCode, campaignCostResult.Message);

            var source = Mapper
                .Map(request)
                .Over(campaignCostResult.Data, cfg =>
                    cfg.Ignore(m => m.Id));
            var entity = Mapper
                .Map(source)
                .ToANew<CampaignCostEntity>(cfg =>
                    cfg.Ignore(m => m.Campaign));

            await CampaignCostDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignCostDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
