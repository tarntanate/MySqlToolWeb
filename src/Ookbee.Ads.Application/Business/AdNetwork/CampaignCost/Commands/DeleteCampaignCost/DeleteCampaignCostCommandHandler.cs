using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.DeleteCampaignCost
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
            await CampaignCostDbRepo.DeleteAsync(request.Id);
            await CampaignCostDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
