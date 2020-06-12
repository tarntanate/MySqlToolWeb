using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public DeleteCampaignCommandHandler(
            IMediator mediator,
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            Mediator = mediator;
            CampaignDbRepo = campaignDbRepo;
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

            await CampaignDbRepo.DeleteAsync(request.Id);
            await CampaignDbRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
