using MediatR;
using Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.DeleteCampaignCost
{
    public class DeleteCampaignCostCommandHandler : IRequestHandler<DeleteCampaignCostCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public DeleteCampaignCostCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            CampaignCostDbRepo = advertiserDbRepo;
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

            await CampaignCostDbRepo.DeleteAsync(request.Id);
            await CampaignCostDbRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
