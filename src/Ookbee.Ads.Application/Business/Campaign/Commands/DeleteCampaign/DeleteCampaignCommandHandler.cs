using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public DeleteCampaignCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo)
        {
            Mediator = mediator;
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteCampaignCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await CampaignEFCoreRepo.DeleteAsync(request.Id);
            await CampaignEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
