using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
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
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public UpdateCampaignCostCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            Mapper = mapper;
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
                {
                    scope.Dispose();
                    return result.Fail(updateCampaignResult.StatusCode, updateCampaignResult.Message);
                }

                var updateCampaignCostResult = await UpdateCampaignCostOnDb(request);
                if (!updateCampaignCostResult.Ok)
                {
                    scope.Dispose();
                    return result.Fail(updateCampaignCostResult.StatusCode, updateCampaignCostResult.Message);
                }

                scope.Complete();
            }

            return result.Success(true);
        }

        private async Task<HttpResult<bool>> UpdateCampaignOnDb(UpdateCampaignCostCommand request)
        {
            var source = Mapper.Map<UpdateCampaignRequest>(request);
            var command = new UpdateCampaignCommand(request.Id, source);
            return await Mediator.Send(command);
        }

        private async Task<HttpResult<bool>> UpdateCampaignCostOnDb(UpdateCampaignCostCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<CampaignCostEntity>(request);
            entity.CampaignId = request.Id;
            await CampaignCostDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignCostDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
