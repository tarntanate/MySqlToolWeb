using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, Response<bool>>
    {
        private readonly AdsDbRepository<CampaignEntity> CampaignDbRepo;

        public DeleteCampaignCommandHandler(
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            await CampaignDbRepo.DeleteAsync(request.Id);
            await CampaignDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
