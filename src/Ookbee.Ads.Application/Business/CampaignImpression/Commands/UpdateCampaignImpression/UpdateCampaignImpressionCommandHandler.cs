using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommandHandler : IRequestHandler<UpdateCampaignImpressionCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public UpdateCampaignImpressionCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updateCampaignResult = await UpdateCampaignOnDb(request, cancellationToken);
                if (!updateCampaignResult.Ok)
                {
                    scope.Dispose();
                    return result.Fail(updateCampaignResult.StatusCode, updateCampaignResult.Message);
                }

                var updateCampaignImpressionResult = await UpdateCampaignImpressionOnDb(request, cancellationToken);
                if (!updateCampaignImpressionResult.Ok)
                {
                    scope.Dispose();
                    return result.Fail(updateCampaignImpressionResult.StatusCode, updateCampaignImpressionResult.Message);
                }

                scope.Complete();
            }

            return result.Success(true);
        }

        private async Task<HttpResult<bool>> UpdateCampaignOnDb(UpdateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var source = Mapper.Map<UpdateCampaignRequest>(request);
            var command = new UpdateCampaignCommand(request.Id, source);
            return await Mediator.Send(command, cancellationToken);
        }

        private async Task<HttpResult<bool>> UpdateCampaignImpressionOnDb(UpdateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<CampaignImpressionEntity>(request);
            entity.CampaignId = request.Id;
            await CampaignImpressionDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignImpressionDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true);
        }
    }
}
