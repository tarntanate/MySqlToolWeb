using MediatR;
using Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.DeleteCampaignCost
{
    public class DeleteCampaignCostCommandHandler : IRequestHandler<DeleteCampaignCostCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignCostEntity> CampaignCostEFCoreRepo { get; }

        public DeleteCampaignCostCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignCostEntity> advertiserEFCoreRepo)
        {
            Mediator = mediator;
            CampaignCostEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignCostCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteCampaignCostCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsCampaignCostByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await CampaignCostEFCoreRepo.DeleteAsync(request.Id);
            await CampaignCostEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
