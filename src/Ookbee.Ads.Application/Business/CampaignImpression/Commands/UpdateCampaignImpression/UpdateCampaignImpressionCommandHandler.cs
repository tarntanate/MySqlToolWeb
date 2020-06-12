using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Application.Business.CampaignImpression.Queries.GetCampaignImpressionById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommandHandler : IRequestHandler<UpdateCampaignImpressionCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public UpdateCampaignImpressionCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateCampaignImpressionCommand request)
        {
            var result = new HttpResult<bool>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updateCampaignResult = await UpdateCampaignOnDb(request);
                if (!updateCampaignResult.Ok)
                    return result.Fail(updateCampaignResult.StatusCode, updateCampaignResult.Message);

                var updateCampaignImpressionResult = await UpdateCampaignImpressionOnDb(request);
                if (!updateCampaignImpressionResult.Ok)
                    return result.Fail(updateCampaignImpressionResult.StatusCode, updateCampaignImpressionResult.Message);

                scope.Complete();

                return result.Success(true);
            }
        }

        private async Task<HttpResult<bool>> UpdateCampaignOnDb(UpdateCampaignImpressionCommand request)
        {
            var command = Mapper
                .Map(request)
                .ToANew<UpdateCampaignCommand>();

            var result = await Mediator.Send(command);

            return result;
        }

        private async Task<HttpResult<bool>> UpdateCampaignImpressionOnDb(UpdateCampaignImpressionCommand request)
        {
            var result = new HttpResult<bool>();

            var campaignImpressionResult = await Mediator.Send(new GetCampaignImpressionByCampaignIdQuery(request.Id));
            if (!campaignImpressionResult.Ok)
                return result.Fail(campaignImpressionResult.StatusCode, campaignImpressionResult.Message);

            var source = Mapper
                .Map(request)
                .Over(campaignImpressionResult.Data, cfg =>
                    cfg.Ignore(m => m.Id));
            var entity = Mapper
                .Map(source)
                .ToANew<CampaignImpressionEntity>(cfg =>
                    cfg.Ignore(m => m.Campaign));

            await CampaignImpressionDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignImpressionDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
