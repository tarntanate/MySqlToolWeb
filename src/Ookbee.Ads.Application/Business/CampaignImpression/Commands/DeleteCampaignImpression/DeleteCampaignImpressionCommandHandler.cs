using MediatR;
using Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommandHandler : IRequestHandler<DeleteCampaignImpressionCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignImpressionEntity> CampaignImpressionEFCoreRepo { get; }

        public DeleteCampaignImpressionCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignImpressionEntity> advertiserEFCoreRepo)
        {
            Mediator = mediator;
            CampaignImpressionEFCoreRepo = advertiserEFCoreRepo;
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

            await CampaignImpressionEFCoreRepo.DeleteAsync(request.Id);
            await CampaignImpressionEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
