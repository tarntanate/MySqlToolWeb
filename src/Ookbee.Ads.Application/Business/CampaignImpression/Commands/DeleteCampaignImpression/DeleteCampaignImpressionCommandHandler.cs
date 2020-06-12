using MediatR;
using Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommandHandler : IRequestHandler<DeleteCampaignImpressionCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public DeleteCampaignImpressionCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignImpressionCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteCampaignImpressionCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsCampaignImpressionByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await CampaignImpressionDbRepo.DeleteAsync(request.Id);
            await CampaignImpressionDbRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
